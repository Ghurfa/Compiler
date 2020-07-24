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
            DisallowInferredFields,
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

            //Resolve inferred field types
            var inferredCheckOptions = CheckExpressionOptions.DisallowInferredFields & CheckExpressionOptions.RequireStatic & CheckExpressionOptions.DisallowDeclarations;
            foreach ((InferredFieldNode node, InferredFieldDeclaration decl) in inferredFields)
            {
                ValueTypeInfo defaultType = CheckExpression(decl.DefaultValue, node, null, inferredCheckOptions);
                node.Type = defaultType;
            }

            //Validate simple defaulted fields
            var simpleCheckOptions = CheckExpressionOptions.RequireStatic & CheckExpressionOptions.DisallowDeclarations;
            foreach ((FieldNode node, SimpleFieldDeclaration decl) in simpleDefaultedFields)
            {
                ValueTypeInfo defaultType = CheckExpression(decl.DefaultValue, node, null, simpleCheckOptions);
                if (defaultType != node.Type) throw new TypeMismatchException();
            }

            //Validate methods and constructors
            foreach (ClassNode node in classes)
            {
                if (node.Methods.Count == 0 && node.Constructors.Count == 0) continue;

                var table = node.GetSymbolsTable();
            }
            ;
        }

        private static void VerifyFunction(ClassNode parent, ParameterListDeclaration parameters, MethodBodyDeclaration body, ValueTypeInfo returnType)
        {
            var locals = new Dictionary<string, ValueTypeInfo>();
            foreach(ParameterDeclaration param in parameters.Parameters)
            {
                locals.Add(param.Identifier.Text, new ValueTypeInfo(param.Type.ToString()));
                
            }
        }

        private static void VerifyScope(MethodNode node, Statement[] statements, Dictionary<string, ValueTypeInfo> locals)
        {
        }

        private static ValueTypeInfo CheckExpression(Expression expr, SymbolNode node, Dictionary<string, ValueTypeInfo> locals, CheckExpressionOptions options)
        {
            switch (expr)
            {
                //Literals
                case IntLiteralExpression _: return new ValueTypeInfo("int");
                case StringLiteralExpression _: return new ValueTypeInfo("string");
                case CharLiteralExpression _: return new ValueTypeInfo("char");
                case TrueLiteralExpression _: return new ValueTypeInfo("bool");
                case FalseLiteralExpression _: return new ValueTypeInfo("bool");

                //Primary expressions
                case IdentifierExpression identifier:
                    {
                        string name = identifier.Identifier.Text;
                        if(locals.TryGetValue(name, out ValueTypeInfo local)) return local;
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                case DeclarationExpression declaration:
                    if (options.HasFlag(CheckExpressionOptions.DisallowDeclarations)) throw new VariablesInFieldDeclarationException();
                    else
                    {
                        string name = declaration.Identifier.Text;
                        if (locals.ContainsKey(name)) throw new DuplicateLocalException();
                        ValueTypeInfo type = new ValueTypeInfo(declaration.Type.ToString());
                        locals.Add(name, type);
                        return type;
                    }
                case PerenthesizedExpression perenthesized:
                    return CheckExpressionRequireType(perenthesized.Expression, node, locals, options);
                case PostIncrementExpression postIncr:
                    return CheckExpressionRequireType(postIncr.BaseExpression, node, locals, options, "int");
                case PostDecrementExpression postDecr:
                    return CheckExpressionRequireType(postDecr.BaseExpression, node, locals, options, "int");

                //Unary expressions
                case BitwiseNotExpression bitwiseNot:
                    return CheckExpressionRequireType(bitwiseNot.BaseExpression, node, locals, options, "int");
                case DereferenceExpression _:
                    throw new NotImplementedException();
                case LogicalNotExpression logicalNot:
                    return CheckExpressionRequireType(logicalNot.BaseExpression, node, locals, options, "bool");
                case PreIncrementExpression preIncr:
                    return CheckExpressionRequireType(preIncr.BaseExpression, node, locals, options, "int");
                case PreDecrementExpression preDecr:
                    return CheckExpressionRequireType(preDecr.BaseExpression, node, locals, options, "int");
                case UnaryPlusExpression unaryPlus:
                    return CheckExpressionRequireType(unaryPlus.BaseExpression, node, locals, options, "int");
                case UnaryMinusExpression unaryMinus:
                    return CheckExpressionRequireType(unaryMinus.BaseExpression, node, locals, options, "int");

                //Assign expressions
                case AssignExpression _:
                case NullCoalescingAssignExpression _:
                case EqualsExpression _:
                case NotEqualsExpression _:
                    return CheckSameTypeExprs(expr.LeftExpr, expr.RightExpr, node, locals, options);
                case DeclAssignExpression declAssign:
                    if (options.HasFlag(CheckExpressionOptions.DisallowDeclarations)) throw new VariablesInFieldDeclarationException();
                    else
                    {
                        string name = declAssign.To.ToString();
                        if (locals.ContainsKey(name)) throw new DuplicateLocalException();
                        ValueTypeInfo type = CheckExpression(declAssign.From, node, locals, options);
                        locals.Add(name, type);
                        return type;
                    }
                case PlusAssignExpression _:
                case MinusAssignExpression _:
                case MultiplyAssignExpression _:
                case DivideAssignExpression _:
                case ModuloAssignExpression _:
                case BitwiseAndAssignExpression _:
                case BitwiseOrAssignExpression _:
                case BitwiseXorAssignExpression _:
                case LeftShiftAssignExpression _:
                case RightShiftAssignExpression _:
                //Int-required binary expressions
                case MinusExpression _:
                case MultiplyExpression _:
                case DivideExpression _:
                case ModuloExpression _:
                case LeftShiftExpression _:
                case RightShiftExpression _:
                case LessThanExpression _:
                case GreaterThanExpression _:
                case LessThanOrEqualToExpression _:
                case GreaterThanOrEqualToExpression _:
                    return CheckSameRequiredTypeExprs(expr.LeftExpr, expr.RightExpr, node, locals, options, "int");
                case BitwiseAndExpression _:
                case BitwiseOrExpression _:
                case BitwiseXorExpression _:
                    return CheckSameRequiredTypeExprs(expr.LeftExpr, expr.RightExpr, node, locals, options, "int", "bool");
                case AndExpression _:
                case OrExpression _:
                    return CheckSameRequiredTypeExprs(expr.LeftExpr, expr.RightExpr, node, locals, options, "bool");
                case PlusExpression _:
                    return CheckSameRequiredTypeExprs(expr.LeftExpr, expr.RightExpr, node, locals, options, "int", "string");
                case IfExpression ifExpr:
                    {
                        CheckExpressionRequireType(ifExpr.Condition, node, locals, options, "bool");
                        return CheckSameTypeExprs(ifExpr.IfTrue, ifExpr.IfFalse, node, locals, options);
                    }
                default: throw new NotImplementedException();
            }
        }

        private static ValueTypeInfo CheckSameTypeExprs(Expression leftExpr, Expression rightExpr, SymbolNode node, Dictionary<string, ValueTypeInfo> locals, CheckExpressionOptions options)
        {
            var leftType = CheckExpression(leftExpr, node, locals, options);
            CheckExpressionRequireType(rightExpr, node, locals, options, leftType.Type);
            return leftType;
        }

        private static ValueTypeInfo CheckSameRequiredTypeExprs(Expression leftExpr, Expression rightExpr, SymbolNode node, Dictionary<string, ValueTypeInfo> locals, CheckExpressionOptions options, params string[] allowedTypes)
        {
            var leftType = CheckExpressionRequireType(leftExpr, node, locals, options, allowedTypes);
            CheckExpressionRequireType(rightExpr, node, locals, options, leftType.Type);
            return leftType;
        }

        private static ValueTypeInfo CheckExpressionRequireType(Expression expr, SymbolNode node, Dictionary<string, ValueTypeInfo> locals, CheckExpressionOptions options, params string[] allowedTypes)
        {
            var type = CheckExpression(expr, node, locals, options);
            if (!allowedTypes.Contains(type.Type)) throw new TypeMismatchException();
            return type;
        }
    }
}
