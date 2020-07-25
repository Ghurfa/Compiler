using Compiler;
using Compiler.SyntaxTreeItems;
using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using TypeChecker.Exceptions;
using TypeChecker.SymbolNodes;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    public static partial class TypeChecker
    {
        public static void CheckNamespace(NamespaceDeclaration namespaceDecl)
        {
            SymbolsTree tree = new SymbolsTree();
            var inferredFields = new List<(InferredFieldNode, InferredFieldDeclaration)>();
            var simpleDefaultedFields = new List<(FieldNode, SimpleFieldDeclaration)>();
            List<ClassNode> classes = new List<ClassNode>();

            //Built symbols tree
            SymbolNode namespaceNode = tree.AddNamespace(namespaceDecl);
            foreach (ClassDeclaration classDecl in namespaceDecl.ClassDeclarations)
            {
                ClassNode classNode = tree.AddClass(classDecl, namespaceNode);
                foreach (ClassItemDeclaration classItem in classDecl.ClassItems)
                {
                    switch (classItem)
                    {
                        case InferredFieldDeclaration iFieldDecl:
                            {
                                var newNode = new InferredFieldNode(iFieldDecl, classNode);
                                inferredFields.Add((newNode, iFieldDecl));
                                classNode.AddFieldChild(newNode);
                            }
                            break;
                        case SimpleFieldDeclaration sFieldDecl:
                            {
                                var newNode = new FieldNode(sFieldDecl, classNode);
                                if (sFieldDecl.DefaultValue != null) simpleDefaultedFields.Add((newNode, sFieldDecl));
                                classNode.AddFieldChild(newNode);
                            }
                            break;
                        case MethodDeclaration methodDecl:
                            classNode.AddMethodChild(new MethodNode(methodDecl, classNode));
                            break;
                        case ConstructorDeclaration ctorDecl:
                            classNode.AddConstructorChild(new ConstructorNode(ctorDecl, classNode));
                            break;
                        default: throw new NotImplementedException();
                    }
                }
                classes.Add(classNode);
            }

            var inferredCheckOptions = VerifyConstraints.DisallowReferences |
                                       VerifyConstraints.RequireStatic |
                                       VerifyConstraints.DisallowDeclarations;

            //Resolve inferred field types
            foreach ((InferredFieldNode node, InferredFieldDeclaration decl) in inferredFields)
            {
                TypeInfo defaultType = VerifyExpression(null, 0, decl.DefaultValue, inferredCheckOptions);
                node.Type = defaultType;
            }

            var simpleCheckOptions = VerifyConstraints.RequireStatic |
                                     VerifyConstraints.DisallowDeclarations;

            //Validate methods and constructors
            foreach (ClassNode node in classes)
            {
                var table = node.GetSymbolsTable();
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
            foreach (ParameterDeclaration param in method.Declaration.ParameterList.Parameters)
            {
                table.AddSymbol(param.Identifier.Text, ValueTypeInfo.Get(param.Type), 0);
            }
            VerifyConstraints options = method.Modifiers.IsStatic ? VerifyConstraints.RequireStatic : 0;
            var scope = new NormalScopeInfo(method.Declaration.Body.Statements, 1);
            scope.Verify(table, method.Type.ReturnType, options);
        }

        private static void VerifyConstructor(SymbolsTable table, ConstructorNode ctor)
        {
            foreach (ParameterDeclaration param in ctor.Declaration.ParameterList.Parameters)
            {
                table.AddSymbol(param.Identifier.Text, ValueTypeInfo.Get(param.Type), 0);
            }
            var scope = new NormalScopeInfo(ctor.Declaration.Body.Statements, 1);
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

        private static TypeInfo VerifyExpression(SymbolsTable table, int index, Expression expr, VerifyConstraints options)
        {
            ValueTypeInfo IntType = ValueTypeInfo.Get("int");
            ValueTypeInfo BoolType = ValueTypeInfo.Get("bool");
            ValueTypeInfo CharType = ValueTypeInfo.Get("char");
            ValueTypeInfo StringType = ValueTypeInfo.Get("string");
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
                    if (options.HasFlag(VerifyConstraints.DisallowReferences)) throw new ReferencingFieldException();
                    else return table.GetSymbol(identifier.Identifier.Text, index);
                case DeclarationExpression declaration:
                    if (options.HasFlag(VerifyConstraints.DisallowDeclarations)) throw new InvalidDeclarationException();
                    else
                    {
                        TypeInfo type = ValueTypeInfo.Get(declaration.Type);
                        table.AddSymbol(declaration.Identifier.Text, type, index);
                        return type;
                    }
                case PerenthesizedExpression perenthesized:
                    return VerifyExpression(table, index, perenthesized.Expression, options);
                case PostIncrementExpression postIncr:
                    return VerifyExpressionRequireType(table, index, postIncr.BaseExpression, options, IntType);
                case PostDecrementExpression postDecr:
                    return VerifyExpressionRequireType(table, index, postDecr.BaseExpression, options, IntType);
                case TupleExpression tupleExpr:
                    {
                        ValueTypeInfo[] subTypes = new ValueTypeInfo[tupleExpr.Values.Items.Length];
                        for (int i = 0; i < subTypes.Length; i++)
                        {
                            TypeInfo type = VerifyExpression(table, index, tupleExpr.Values.Items[i].Expression, options);
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
                    return VerifyExpressionRequireType(table, index, bitwiseNot.BaseExpression, options, IntType);
                case DereferenceExpression _:
                    throw new NotImplementedException();
                case LogicalNotExpression logicalNot:
                    return VerifyExpressionRequireType(table, index, logicalNot.BaseExpression, options, BoolType);
                case PreIncrementExpression preIncr:
                    return VerifyExpressionRequireType(table, index, preIncr.BaseExpression, options, IntType);
                case PreDecrementExpression preDecr:
                    return VerifyExpressionRequireType(table, index, preDecr.BaseExpression, options, IntType);
                case UnaryPlusExpression unaryPlus:
                    return VerifyExpressionRequireType(table, index, unaryPlus.BaseExpression, options, IntType);
                case UnaryMinusExpression unaryMinus:
                    return VerifyExpressionRequireType(table, index, unaryMinus.BaseExpression, options, IntType);

                case NullCoalescingExpression _:
                //Assign expressions
                case AssignExpression _:
                case NullCoalescingAssignExpression _:
                    return VerifySameTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, options);

                case DeclAssignExpression declAssign:
                    if (options.HasFlag(VerifyConstraints.DisallowDeclarations)) throw new InvalidDeclarationException();
                    else
                    {
                        string name = declAssign.To.ToString();
                        TypeInfo type = VerifyExpression(table, index, declAssign.From, options);
                        table.AddSymbol(name, type, index);
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
                    return VerifySameRequiredTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, options, IntType);

                //Comparison operators
                case LessThanExpression _:
                case GreaterThanExpression _:
                case LessThanOrEqualToExpression _:
                case GreaterThanOrEqualToExpression _:
                    VerifySameRequiredTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, options, IntType);
                    return BoolType;
                case EqualsExpression _:
                case NotEqualsExpression _:
                    VerifySameTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, options);
                    return BoolType;

                case BitwiseAndExpression _:
                case BitwiseOrExpression _:
                case BitwiseXorExpression _:
                    return VerifySameRequiredTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, options, IntType, BoolType);

                case AndExpression _:
                case OrExpression _:
                    return VerifySameRequiredTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, options, BoolType);

                case PlusAssignExpression _:
                case PlusExpression _:
                    return VerifySameRequiredTypeExprs(table, index, expr.LeftExpr, expr.RightExpr, options, IntType, StringType, CharType);

                case IfExpression ifExpr:
                    {
                        VerifyExpressionRequireType(table, index, ifExpr.Condition, options, BoolType);
                        return VerifySameTypeExprs(table, index, ifExpr.IfTrue, ifExpr.IfFalse, options);
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
            if (!allowedTypes.Contains(type)) throw new TypeMismatchException();
            return type;
        }
    }
}
