using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TypeChecker.Exceptions;
using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using SymbolsTable;
using Parser.SyntaxTreeItems;
using Parser.SyntaxTreeItems.ClassItemDeclarations;
using Parser;
using Parser.SyntaxTreeItems.Statements;

namespace TypeChecker
{
    public static partial class TypeChecker
    {
        private static SymbolsTableBuilder builder = new SymbolsTableBuilder();
        public static SymbolsTable.SymbolsTable Table { get => builder; }

        public static void CheckNamespace(NamespaceDeclaration namespaceDecl)
        {
            builder.BuildTree(namespaceDecl.AsEnumerable(), out List<(InferredField, InferredFieldDeclaration)> inferredFields);

            var inferredCheckOptions = VerifyConstraints.DisallowReferences |
                                       VerifyConstraints.RequireStatic |
                                       VerifyConstraints.DisallowDeclarations;

            //Resolve inferred field types
            foreach ((InferredField node, InferredFieldDeclaration decl) in inferredFields)
            {
                TypeInfo defaultType = VerifyExpression(null, 0, decl.DefaultValue, inferredCheckOptions);
                if (defaultType is VoidTypeInfo)
                    throw new VoidInferredFieldException();
                else if (defaultType is ValueTypeInfo valTypeInfo)
                    node.Type = valTypeInfo;
                else throw new InvalidOperationException();
            }

            var simpleCheckOptions = VerifyConstraints.RequireStatic |
                                     VerifyConstraints.DisallowDeclarations;

            bool hasEntryPoint = false;
            //Validate other class members
            foreach (ClassNode node in Table.IterateWithStack)
            {
                foreach (SimpleField field in node.SimpleDefaultedFields)
                {
                    SimpleFieldDeclaration sFieldDecl = field.Declaration;
                    VerifyExpressionRequireType(builder, 0, sFieldDecl.DefaultValue, simpleCheckOptions, field.Type);
                }
                foreach (Method method in node.Methods)
                {
                    VerifyMethod(builder, method);
                    if (method.Name == "Main" && method.Modifiers.IsStatic)
                    {
                        if (hasEntryPoint) throw new MultipleEntryPointsException();
                        else hasEntryPoint = true;
                    }
                }
                foreach (Constructor ctor in node.Constructors) VerifyConstructor(builder, ctor);
            }

            if (!hasEntryPoint) throw new MissingEntryPointException();
        }

        private static void VerifyMethod(SymbolsTableBuilder table, Method method)
        {
            VerifyConstraints constraints = method.Modifiers.IsStatic ? VerifyConstraints.RequireStatic : 0;
            var statements = method.Declaration.Body.Statements;
            table.EnterFunction(method);

            List<IScopeInfo> childScopes = new List<IScopeInfo>();
            for (int i = 0; i < statements.Length; i++)
            {
                VerifyStatement(table, i, childScopes, statements[i], method.Type.ReturnType, constraints);
            }

            foreach (IScopeInfo childScope in childScopes)
            {
                childScope.Verify(table, method.Type.ReturnType, constraints);
            }

            table.ExitFunction();
        }

        private static void VerifyConstructor(SymbolsTableBuilder table, Constructor ctor)
        {
            var statements = ctor.Declaration.Body.Statements;
            table.EnterFunction(ctor);

            List<IScopeInfo> childScopes = new List<IScopeInfo>();
            for (int i = 0; i < statements.Length; i++)
            {
                VerifyStatement(table, i, childScopes, statements[i], VoidTypeInfo.Get(), 0);
            }

            foreach (IScopeInfo childScope in childScopes)
            {
                childScope.Verify(table, VoidTypeInfo.Get(), 0);
            }

            table.ExitFunction();
        }

        private static void VerifyStatement(SymbolsTableBuilder table, int indexInParent, List<IScopeInfo> scopes, Statement statement, TypeInfo returnType, VerifyConstraints constraints)
        {
            switch (statement)
            {
                case CodeBlock codeBlock: scopes.Add(new NormalScopeInfo(codeBlock.Statements, indexInParent)); break;
                case EmptyStatement _: break;
                case ExpressionStatement exprStatement: VerifyExpression(table, indexInParent, exprStatement.Expression, constraints); break;
                case IfBlock ifBlock:
                    VerifyExpressionRequireType(table, indexInParent, ifBlock.Condition, constraints, ValueTypeInfo.PrimitiveTypes["bool"]);
                    VerifyStatement(table, indexInParent, scopes, ifBlock.IfTrue, returnType, constraints);
                    if(ifBlock.IfFalse != null)
                    {
                        VerifyStatement(table, indexInParent, scopes, ifBlock.IfFalse, returnType, constraints);
                    }
                    break;
                case ForBlock forBlock: scopes.Add(new ForScopeInfo(forBlock, indexInParent)); break;
                case WhileBlock whileBlock:
                    VerifyExpressionRequireType(table, indexInParent, whileBlock.Condition, constraints, ValueTypeInfo.PrimitiveTypes["bool"]);
                    VerifyStatement(table, indexInParent, scopes, whileBlock.Body, returnType, constraints);
                    break;
                case ReturnStatement retStatement:
                    {
                        if (returnType is VoidTypeInfo) throw new UnexpectedReturnValueException();
                        var retType = VerifyExpression(table, indexInParent, retStatement.Expression, constraints);
                        if (retType != returnType) throw new InvalidReturnTypeException();
                    }
                    break;
                case ExitStatement _:
                    if (!(returnType is VoidTypeInfo)) throw new InvalidExitStatementException();
                    break;
                default: throw new NotImplementedException();
            }
        }

        private static TypeInfo VerifyExpression(SymbolsTableBuilder table, int index, Expression expr, VerifyConstraints constraints)
        {
            ValueTypeInfo IntType = ValueTypeInfo.PrimitiveTypes["int"];
            ValueTypeInfo BoolType = ValueTypeInfo.PrimitiveTypes["bool"];
            ValueTypeInfo CharType = ValueTypeInfo.PrimitiveTypes["char"];
            ValueTypeInfo StringType = ValueTypeInfo.PrimitiveTypes["string"];
            switch (expr)
            {
                //Literals
                case IntLiteralExpression _: return IntType;
                case StringLiteralExpression _: return StringType;
                case CharLiteralExpression _: return CharType;
                case TrueLiteralExpression _: return BoolType;
                case FalseLiteralExpression _: return BoolType;

                //Primary expressions
                case IdentifierExpression identifier:
                    if (constraints.HasFlag(VerifyConstraints.DisallowReferences))
                        throw new ReferencingFieldException();
                    else
                    {
                        var requireStatic = constraints.HasFlag(VerifyConstraints.RequireStatic);
                        string name = identifier.Identifier.Text;
                        if (table.GetLocal(name, index, out Local local) == Result.Success)
                        {
                            return local.Type;
                        }
                        else
                        {
                            Handle(table.GetField(name, requireStatic, identifier, out Field field));
                            return field.Type;
                        }
                    }
                case DeclarationExpression declaration:
                    if (constraints.HasFlag(VerifyConstraints.DisallowDeclarations)) throw new InvalidDeclarationException();
                    else
                    {
                        ValueTypeInfo type = ValueTypeInfo.Get(table, declaration.Type);
                        table.AddLocal(declaration.Identifier.Text, type, index);
                        return type;
                    }
                case MemberAccessExpression memberAccess:
                    if (memberAccess.BaseExpression is IdentifierExpression baseIdentifier)
                    {
                        Handle(table.GetField(baseIdentifier.Identifier.Text, memberAccess.Item.Text, index, memberAccess, out Field field));
                        return field.Type;
                    }
                    else
                    {
                        TypeInfo baseType = VerifyExpression(table, index, memberAccess.BaseExpression, constraints);

                        if (baseType is VoidTypeInfo) throw new VoidMemberAccessException();
                        else if (baseType is ValueTypeInfo valueTypeInfo)
                        {
                            Handle(table.GetField(valueTypeInfo.Class, memberAccess.Item.Text, false, memberAccess, out Field field));
                            return field.Type;
                        }
                        else throw new InvalidOperationException();
                    }
                case MethodCallExpression methodCall:
                    {
                        ValueTypeInfo[] argTypes = GetArgTypes(table, index, methodCall.Arguments, constraints);
                        if (methodCall.Method is IdentifierExpression identifierExpr)
                        {
                            Handle(table.GetMethod(identifierExpr.Identifier.Text, argTypes, methodCall, out Method method));
                            return method.Type.ReturnType;
                        }
                        else if (methodCall.Method is MemberAccessExpression memberAccess)
                        {
                            string methodName = memberAccess.Item.Text;
                            if (memberAccess.BaseExpression is IdentifierExpression identifierBase)
                            {
                                string baseIdText = identifierBase.Identifier.Text;
                                if (table.GetLocal(baseIdText, index, out Local baseLocal) == Result.Success)
                                {
                                    Handle(table.GetMethod(baseLocal.Type.Class, methodName, argTypes, methodCall, out Method method));
                                    return method.Type.ReturnType;
                                }
                                else if (table.GetField(baseIdText, constraints.HasFlag(VerifyConstraints.RequireStatic), identifierBase, out Field baseField) == Result.Success)
                                {
                                    Handle(table.GetMethod(baseField.Type.Class, methodName, argTypes, methodCall, out Method method));
                                    return method.Type.ReturnType;
                                }
                                else if (table.GetClass(baseIdText, out ClassNode classNode) == Result.Success)
                                {
                                    Handle(table.GetMethod(classNode, methodName, argTypes, methodCall, out Method method));
                                    if (!method.Modifiers.IsStatic) throw new InvalidInstanceReferenceException();
                                    return method.Type.ReturnType;
                                }
                                else throw new LocalNotFoundException();
                            }
                            else if(memberAccess.BaseExpression is PrimitiveTypeExpression primTypeBase)
                            {
                                ClassNode classNode = ValueTypeInfo.PrimitiveTypes[primTypeBase.PrimitiveType.Text].Class;
                                Handle(table.GetMethod(classNode, methodName, argTypes, methodCall, out Method method));
                                if (!method.Modifiers.IsStatic) throw new InvalidInstanceReferenceException();
                                return method.Type.ReturnType;
                            }
                            else
                            {
                                TypeInfo baseType = VerifyExpression(table, index, memberAccess.BaseExpression, constraints);

                                if (baseType is VoidTypeInfo) throw new VoidMemberAccessException();
                                else if (baseType is ValueTypeInfo valueTypeInfo)
                                {
                                    Handle(table.GetMethod(valueTypeInfo.Class, methodName, argTypes, methodCall, out Method method));
                                    return method.Type.ReturnType;
                                }
                                else throw new InvalidOperationException();
                            }
                        }
                        else if (methodCall.Method is NullCondMemberAccessExpression nullCondMemberAccess)
                        {
                            throw new NotImplementedException();
                        }
                        else throw new InvalidOperationException();
                    }
                case NewObjectExpression newObject:
                    {
                        ValueTypeInfo type = ValueTypeInfo.Get(table, newObject.Type);
                        ValueTypeInfo[] argTypes = GetArgTypes(table, index, newObject.Arguments, constraints);
                        ClassNode node = type.Class;
                        Handle(table.GetConstructor(node, argTypes, newObject, out Constructor _));
                        return type;
                    }
                case PerenthesizedExpression perenthesized:
                    return VerifyExpression(table, index, perenthesized.Expression, constraints);
                case PostIncrementExpression postIncr:
                    return VerifyExpressionRequireType(table, index, postIncr.BaseExpression, constraints, IntType);
                case PostDecrementExpression postDecr:
                    return VerifyExpressionRequireType(table, index, postDecr.BaseExpression, constraints, IntType);
                case TupleExpression tupleExpr:
                    {
                        ValueTypeInfo[] subTypes = new ValueTypeInfo[tupleExpr.Values.Items.Length];
                        for (int i = 0; i < subTypes.Length; i++)
                        {
                            TypeInfo type = VerifyExpression(table, index, tupleExpr.Values.Items[i].Expression, constraints);
                            if (type is ValueTypeInfo valType)
                            {
                                subTypes[i] = valType;
                            }
                            else throw new InvalidTupleItemException();
                        }
                        return TupleTypeInfo.Get(subTypes);
                    }

                //Unary expressions
                case BitwiseNotExpression bitwiseNot:
                    return VerifyExpressionRequireType(table, index, bitwiseNot.BaseExpression, constraints, IntType);
                case DereferenceExpression _:
                    throw new NotImplementedException();
                case LogicalNotExpression logicalNot:
                    return VerifyExpressionRequireType(table, index, logicalNot.BaseExpression, constraints, BoolType);
                case PreIncrementExpression preIncr:
                    return VerifyExpressionRequireType(table, index, preIncr.BaseExpression, constraints, IntType);
                case PreDecrementExpression preDecr:
                    return VerifyExpressionRequireType(table, index, preDecr.BaseExpression, constraints, IntType);
                case UnaryPlusExpression unaryPlus:
                    return VerifyExpressionRequireType(table, index, unaryPlus.BaseExpression, constraints, IntType);
                case UnaryMinusExpression unaryMinus:
                    return VerifyExpressionRequireType(table, index, unaryMinus.BaseExpression, constraints, IntType);

                case NullCoalescingExpression _:
                //Assign expressions
                case AssignExpression _:
                case NullCoalescingAssignExpression _:
                    return VerifySameTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, constraints);

                case DeclAssignExpression declAssign:
                    if (constraints.HasFlag(VerifyConstraints.DisallowDeclarations)) throw new InvalidDeclarationException();
                    else
                    {
                        string name = declAssign.To.ToString();
                        TypeInfo type = VerifyExpression(table, index, declAssign.From, constraints);
                        if (type is VoidTypeInfo) throw new VoidVariableDeclarationException();
                        else if (type is ValueTypeInfo valTypeInfo) Handle(table.AddLocal(name, valTypeInfo, index));
                        else throw new InvalidOperationException();
                        return type;
                    }

                //Int-required binary expressions
                case MinusAssignExpression _:
                case MultiplyAssignExpression _:
                case DivideAssignExpression _:
                case ModuloAssignExpression _:
                case BitwiseAndAssignExpression _:
                case BitwiseOrAssignExpression _:
                case BitwiseXorAssignExpression _:
                case LeftShiftAssignExpression _:
                case RightShiftAssignExpression _:
                case MinusExpression _:
                case MultiplyExpression _:
                case DivideExpression _:
                case ModuloExpression _:
                case LeftShiftExpression _:
                case RightShiftExpression _:
                    return VerifySameRequiredTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, constraints, IntType);

                //Comparison operators
                case LessThanExpression _:
                case GreaterThanExpression _:
                case LessThanOrEqualToExpression _:
                case GreaterThanOrEqualToExpression _:
                    VerifySameRequiredTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, constraints, IntType);
                    return BoolType;
                case EqualsExpression _:
                case NotEqualsExpression _:
                    VerifySameTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, constraints);
                    return BoolType;

                case BitwiseAndExpression _:
                case BitwiseOrExpression _:
                case BitwiseXorExpression _:
                    return VerifySameRequiredTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, constraints, IntType, BoolType);

                case AndExpression _:
                case OrExpression _:
                    return VerifySameRequiredTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, constraints, BoolType);

                case PlusAssignExpression _:
                case PlusExpression _:
                    return VerifySameRequiredTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, constraints, IntType, StringType, CharType);

                case IfExpression ifExpr:
                    {
                        VerifyExpressionRequireType(table, index, ifExpr.Condition, constraints, BoolType);
                        return VerifySameTypeExprs(table, index, ifExpr.IfTrue, ifExpr.IfFalse, constraints);
                    }
                default: throw new NotImplementedException();
            }
        }

        private static TypeInfo VerifySameTypeExprs(SymbolsTableBuilder table, int index, Expression leftExpr, Expression rightExpr, VerifyConstraints constraints)
        {
            var leftType = VerifyExpression(table, index, leftExpr, constraints);
            VerifyExpressionRequireType(table, index, rightExpr, constraints, leftType);
            return leftType;
        }

        private static TypeInfo VerifySameRequiredTypeExprs(SymbolsTableBuilder table, int index, Expression leftExpr, Expression rightExpr, VerifyConstraints constraints, params TypeInfo[] allowedTypes)
        {
            var leftType = VerifyExpressionRequireType(table, index, leftExpr, constraints, allowedTypes);
            VerifyExpressionRequireType(table, index, rightExpr, constraints, leftType);
            return leftType;
        }

        private static TypeInfo VerifyExpressionRequireType(SymbolsTableBuilder table, int index, Expression expr, VerifyConstraints constraints, params TypeInfo[] allowedTypes)
        {
            var type = VerifyExpression(table, index, expr, constraints);
            foreach (TypeInfo allowedType in allowedTypes)
            {
                if (type.IsConvertibleTo(allowedType))
                {
                    return type;
                }
            }
            throw new TypeMismatchException();
        }

        private static ValueTypeInfo[] GetArgTypes(SymbolsTableBuilder table, int index, ArgumentList arguments, VerifyConstraints constraints)
        {
            ValueTypeInfo[] ret = new ValueTypeInfo[arguments.Arguments.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                TypeInfo exprType = VerifyExpression(table, index, arguments.Arguments[i].Expression, constraints);
                if (exprType is VoidTypeInfo) throw new VoidArgumentException();
                else if (exprType is ValueTypeInfo valType) ret[i] = valType;
                else throw new InvalidOperationException();
            }
            return ret;
        }

        private static void Handle(Result result)
        {
            if (result != Result.Success) throw new InvalidOperationException();
            //Todo: handle
        }
    }
}
