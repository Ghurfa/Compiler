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
    public static class TypeChecker
    {
        [Flags]
        private enum CheckExpressionOptions
        {
            RequireStatic,
            DisallowFieldReferences,
            DisallowDeclarations
        }

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

            var inferredCheckOptions = CheckExpressionOptions.DisallowFieldReferences &
                                       CheckExpressionOptions.RequireStatic &
                                       CheckExpressionOptions.DisallowDeclarations;

            //Resolve inferred field types
            foreach ((InferredFieldNode node, InferredFieldDeclaration decl) in inferredFields)
            {
                TypeInfo defaultType = VerifyExpression(null, node, decl.DefaultValue, inferredCheckOptions);
                node.Type = defaultType;
            }


            var simpleCheckOptions = CheckExpressionOptions.RequireStatic &
                                     CheckExpressionOptions.DisallowDeclarations;

            //Validate methods and constructors
            foreach (ClassNode node in classes)
            {
                if (node.Methods.Count == 0 && node.Constructors.Count == 0) continue;

                var table = node.GetSymbolsTable();
                foreach (FieldNode field in node.SimpleDefaultedFields)
                {
                    SimpleFieldDeclaration sFieldDecl = (SimpleFieldDeclaration)field.Declaration;
                    VerifyExpressionRequireType(table, node, sFieldDecl.DefaultValue, simpleCheckOptions, field.Type);
                }
                foreach (MethodNode method in node.Methods) VerifyMethod(table, node, method);
                foreach (ConstructorNode ctor in node.Constructors) VerifyConstructor(table, node, ctor);
            }
            ;
        }

        private static void VerifyMethod(SymbolsTable table, ClassNode parent, MethodNode method)
        {
            foreach (ParameterDeclaration param in method.Declaration.ParameterList.Parameters)
            {
                table.AddSymbol(param.Identifier.Text, ValueTypeInfo.Get(param.Type));
            }
            CheckExpressionOptions options = method.Modifiers.IsStatic ? CheckExpressionOptions.RequireStatic : 0;
            VerifyScope(table, method, method.Declaration.Body.Statements, method.Type.ReturnType, options);
        }

        private static void VerifyConstructor(SymbolsTable table, ClassNode parent, ConstructorNode ctor)
        {
            foreach (ParameterDeclaration param in ctor.Declaration.ParameterList.Parameters)
            {
                table.AddSymbol(param.Identifier.Text, ValueTypeInfo.Get(param.Type));
            }
            VerifyScope(table, ctor, ctor.Declaration.Body.Statements, VoidTypeInfo.Get(), 0);
        }

        private static void VerifyScope(SymbolsTable table, SymbolNode node, Statement[] statements, TypeInfo returnType, CheckExpressionOptions exprOptions, bool enterScope = true)
        {
            if (enterScope) table.EnterScope();

            foreach (Statement statement in statements) VerifyStatement(table, node, statement, returnType, exprOptions);

            table.ExitScope();
        }

        private static void VerifyStatement(SymbolsTable table, SymbolNode node, Statement statement, TypeInfo returnType, CheckExpressionOptions exprOptions)
        {
            switch (statement)
            {
                case CodeBlock codeBlock: VerifyScope(table, node, codeBlock.Statements, returnType, exprOptions); break;
                case EmptyStatement _: break;
                case ExpressionStatement exprStatement: VerifyExpression(table, node, exprStatement.Expression, exprOptions); break;
                case ForBlock forBlock:
                    table.EnterScope();
                    VerifyStatement(table, node, forBlock.StartStatement, returnType, exprOptions);
                    VerifyExpressionRequireType(table, node, forBlock.ContinueExpr, exprOptions, ValueTypeInfo.Get("bool"));
                    VerifyStatement(table, node, forBlock.IterateStatement, returnType, exprOptions);
                    VerifyStatement(table, node, forBlock.Body, returnType, exprOptions);
                    break;
                case ReturnStatement retStatement:
                    {
                        if (returnType is VoidTypeInfo) throw new UnexpectedReturnValueException();
                        var retType = VerifyExpression(table, node, retStatement.Expression, exprOptions);
                        if (retType != returnType)  throw new InvalidReturnTypeException();
                    }
                    break;
                case ExitStatement _:
                    if (!(returnType is VoidTypeInfo)) throw new InvalidExitStatementException();
                    break;
                default: throw new NotImplementedException();
            }
        }

        private static TypeInfo VerifyExpression(SymbolsTable table, SymbolNode node, Expression expr, CheckExpressionOptions options)
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
                    if (options.HasFlag(CheckExpressionOptions.DisallowFieldReferences)) throw new ReferencingFieldException();
                    else return table.GetSymbol(identifier.Identifier.Text);
                case DeclarationExpression declaration:
                    if (options.HasFlag(CheckExpressionOptions.DisallowDeclarations)) throw new VariablesInFieldDeclarationException();
                    else
                    {
                        TypeInfo type = ValueTypeInfo.Get(declaration.Type);
                        table.AddSymbol(declaration.Identifier.Text, type);
                        return type;
                    }
                case PerenthesizedExpression perenthesized:
                    return VerifyExpressionRequireType(table, node, perenthesized.Expression, options);
                case PostIncrementExpression postIncr:
                    return VerifyExpressionRequireType(table, node, postIncr.BaseExpression, options, IntType);
                case PostDecrementExpression postDecr:
                    return VerifyExpressionRequireType(table, node, postDecr.BaseExpression, options, IntType);
                case TupleExpression tupleExpr:
                    {
                        ValueTypeInfo[] subTypes = new ValueTypeInfo[tupleExpr.Values.Items.Length];
                        for (int i = 0; i < subTypes.Length; i++)
                        {
                            TypeInfo type = VerifyExpression(table, node, tupleExpr.Values.Items[i].Expression, options);
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
                    return VerifyExpressionRequireType(table, node, bitwiseNot.BaseExpression, options, IntType);
                case DereferenceExpression _:
                    throw new NotImplementedException();
                case LogicalNotExpression logicalNot:
                    return VerifyExpressionRequireType(table, node, logicalNot.BaseExpression, options, BoolType);
                case PreIncrementExpression preIncr:
                    return VerifyExpressionRequireType(table, node, preIncr.BaseExpression, options, IntType);
                case PreDecrementExpression preDecr:
                    return VerifyExpressionRequireType(table, node, preDecr.BaseExpression, options, IntType);
                case UnaryPlusExpression unaryPlus:
                    return VerifyExpressionRequireType(table, node, unaryPlus.BaseExpression, options, IntType);
                case UnaryMinusExpression unaryMinus:
                    return VerifyExpressionRequireType(table, node, unaryMinus.BaseExpression, options, IntType);

                case NullCoalescingExpression _:
                //Assign expressions
                case AssignExpression _:
                case NullCoalescingAssignExpression _:
                    return VerifySameTypeExprs(table, node, expr.LeftExpr, expr.RightExpr, options);

                case DeclAssignExpression declAssign:
                    if (options.HasFlag(CheckExpressionOptions.DisallowDeclarations)) throw new VariablesInFieldDeclarationException();
                    else
                    {
                        string name = declAssign.To.ToString();
                        TypeInfo type = VerifyExpression(table, node, declAssign.From, options);
                        table.AddSymbol(name, type);
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
                    return VerifySameRequiredTypeExprs(table, node, expr.LeftExpr, expr.RightExpr, options, IntType);

                //Comparison operators
                case LessThanExpression _:
                case GreaterThanExpression _:
                case LessThanOrEqualToExpression _:
                case GreaterThanOrEqualToExpression _:
                    VerifySameRequiredTypeExprs(table, node, expr.LeftExpr, expr.RightExpr, options, IntType);
                    return BoolType;
                case EqualsExpression _:
                case NotEqualsExpression _:
                    VerifySameTypeExprs(table, node, expr.LeftExpr, expr.RightExpr, options);
                    return BoolType;

                case BitwiseAndExpression _:
                case BitwiseOrExpression _:
                case BitwiseXorExpression _:
                    return VerifySameRequiredTypeExprs(table, node, expr.LeftExpr, expr.RightExpr, options, IntType, BoolType);

                case AndExpression _:
                case OrExpression _:
                    return VerifySameRequiredTypeExprs(table, node, expr.LeftExpr, expr.RightExpr, options, BoolType);

                case PlusAssignExpression _:
                case PlusExpression _:
                    return VerifySameRequiredTypeExprs(table, node, expr.LeftExpr, expr.RightExpr, options, IntType, StringType, CharType);

                case IfExpression ifExpr:
                    {
                        VerifyExpressionRequireType(table, node, ifExpr.Condition, options, BoolType);
                        return VerifySameTypeExprs(table, node, ifExpr.IfTrue, ifExpr.IfFalse, options);
                    }
                default: throw new NotImplementedException();
            }
        }

        private static TypeInfo VerifySameTypeExprs(SymbolsTable table, SymbolNode node, Expression leftExpr, Expression rightExpr, CheckExpressionOptions options)
        {
            var leftType = VerifyExpression(table, node, leftExpr, options);
            VerifyExpressionRequireType(table, node, rightExpr, options, leftType);
            return leftType;
        }

        private static TypeInfo VerifySameRequiredTypeExprs(SymbolsTable table, SymbolNode node, Expression leftExpr, Expression rightExpr, CheckExpressionOptions options, params TypeInfo[] allowedTypes)
        {
            var leftType = VerifyExpressionRequireType(table, node, leftExpr, options, allowedTypes);
            VerifyExpressionRequireType(table, node, rightExpr, options, leftType);
            return leftType;
        }

        private static TypeInfo VerifyExpressionRequireType(SymbolsTable table, SymbolNode node, Expression expr, CheckExpressionOptions options, params TypeInfo[] allowedTypes)
        {
            var type = VerifyExpression(table, node, expr, options);
            if (!allowedTypes.Contains(type)) throw new TypeMismatchException();
            return type;
        }
    }
}
