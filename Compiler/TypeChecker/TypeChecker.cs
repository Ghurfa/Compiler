using Compiler;
using Compiler.SyntaxTreeItems;
using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TypeChecker.Exceptions;
using TypeChecker.SymbolNodes;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    public static partial class TypeChecker
    {
        public static void CheckNamespace(NamespaceDeclaration namespaceDecl)
        {
            SymbolsTable table = new SymbolsTable();

            table.BuildTree(namespaceDecl.AsEnumerable(), out List<(InferredFieldNode, InferredFieldDeclaration)> inferredFields);

            var inferredCheckOptions = VerifyConstraints.DisallowReferences |
                                       VerifyConstraints.RequireStatic |
                                       VerifyConstraints.DisallowDeclarations;

            //Resolve inferred field types
            foreach ((InferredFieldNode node, InferredFieldDeclaration decl) in inferredFields)
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

            //Validate methods and constructors
            foreach (ClassNode node in table.Iterate)
            {
                foreach (FieldNode field in node.SimpleDefaultedFields)
                {
                    SimpleFieldDeclaration sFieldDecl = (SimpleFieldDeclaration)field.Declaration;
                    VerifyExpressionRequireType(table, 0, sFieldDecl.DefaultValue, simpleCheckOptions, field.Type);
                }
                foreach (MethodNode method in node.Methods) VerifyMethod(table, method);
                foreach (ConstructorNode ctor in node.Constructors) VerifyConstructor(table, ctor);
            }
            ;
        }

        private static void VerifyMethod(SymbolsTable table, MethodNode method)
        {
            VerifyConstraints options = method.Modifiers.IsStatic ? VerifyConstraints.RequireStatic : 0;
            var scope = new FunctionScopeInfo(method.Declaration.ParameterList, method.Declaration.Body.Statements);
            scope.Verify(table, method.Type.ReturnType, options);
        }

        private static void VerifyConstructor(SymbolsTable table, ConstructorNode ctor)
        {
            var scope = new FunctionScopeInfo(ctor.Declaration.ParameterList, ctor.Declaration.Body.Statements);
            scope.Verify(table, VoidTypeInfo.Get(), 0);
        }

        private static void VerifyStatement(SymbolsTable table, int indexInParent, List<IScopeInfo> scopes, Statement statement, TypeInfo returnType, VerifyConstraints constraints)
        {
            switch (statement)
            {
                case CodeBlock codeBlock: scopes.Add(new NormalScopeInfo(codeBlock.Statements, indexInParent)); break;
                case EmptyStatement _: break;
                case ExpressionStatement exprStatement: VerifyExpression(table, indexInParent, exprStatement.Expression, constraints); break;
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

        private static TypeInfo VerifyExpression(SymbolsTable table, int index, Expression expr, VerifyConstraints constraints)
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
                        if (table.TryGetSymbol(identifier.Identifier.Text, index, out ValueTypeInfo type, requireStatic))
                            return type;
                        else throw new LocalNotFoundException();
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
                        return table.GetFieldType(baseIdentifier.Identifier.Text, memberAccess.Item.Text, index);
                    else
                    {
                        TypeInfo baseType = VerifyExpression(table, index, memberAccess.BaseExpression, constraints);

                        if (baseType is VoidTypeInfo) throw new VoidMemberAccessException();
                        else if (baseType is ValueTypeInfo valueTypeInfo)
                            return table.GetInstanceFieldType(valueTypeInfo, memberAccess.Item.Text);
                        else throw new InvalidOperationException();
                    }
                case MethodCallExpression methodCall:
                    {
                        ValueTypeInfo[] argTypes = new ValueTypeInfo[methodCall.Arguments.Arguments.Length];
                        if (methodCall.Method is IdentifierExpression identifierExpr)
                        {
                            if (table.TryGetMethodType(identifierExpr.Identifier.Text, argTypes, out TypeInfo type))
                                return type;
                            else throw new NoSuchMemberException();
                        }
                        else if (methodCall.Method is MemberAccessExpression memberAccess)
                        {
                            TypeInfo baseType = VerifyExpression(table, index, memberAccess.BaseExpression, constraints);

                            if (baseType is VoidTypeInfo) throw new VoidMemberAccessException();
                            else if (baseType is ValueTypeInfo valueTypeInfo)
                            {
                                if (table.TryGetMethodType(valueTypeInfo.Class, memberAccess.Item.Text, argTypes, out TypeInfo type))
                                    return type;
                                else throw new NoSuchMemberException();
                            }
                            else throw new InvalidOperationException();
                        }
                        else if (methodCall.Method is NullCondMemberAccessExpression nullCondMemberAccess)
                        {
                            TypeInfo baseType = VerifyExpression(table, index, nullCondMemberAccess.BaseExpression, constraints);

                            if (baseType is VoidTypeInfo) throw new VoidMemberAccessException();
                            else if (baseType is ValueTypeInfo valueTypeInfo)
                            {
                                if (table.TryGetMethodType(valueTypeInfo.Class, nullCondMemberAccess.Item.Text, argTypes, out TypeInfo type))
                                    return type;
                                else throw new NoSuchMemberException();
                            }
                            else throw new InvalidOperationException();
                        }
                        else throw new InvalidOperationException();
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
                        else if (type is ValueTypeInfo valTypeInfo)
                            table.AddLocal(name, valTypeInfo, index);
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

        private static TypeInfo VerifySameTypeExprs(SymbolsTable table, int index, Expression leftExpr, Expression rightExpr, VerifyConstraints options)
        {
            var leftType = VerifyExpression(table, index, leftExpr, options);
            VerifyExpressionRequireType(table, index, rightExpr, options, leftType);
            return leftType;
        }

        private static TypeInfo VerifySameRequiredTypeExprs(SymbolsTable table, int index, Expression leftExpr, Expression rightExpr, VerifyConstraints options, params TypeInfo[] allowedTypes)
        {
            var leftType = VerifyExpressionRequireType(table, index, leftExpr, options, allowedTypes);
            VerifyExpressionRequireType(table, index, rightExpr, options, leftType);
            return leftType;
        }

        private static TypeInfo VerifyExpressionRequireType(SymbolsTable table, int index, Expression expr, VerifyConstraints options, params TypeInfo[] allowedTypes)
        {
            var type = VerifyExpression(table, index, expr, options);
            foreach(TypeInfo allowedType in allowedTypes)
            {
                if(type.IsConvertibleTo(allowedType))
                {
                    return type;
                }
            }
            throw new TypeMismatchException();
        }
    }
}
