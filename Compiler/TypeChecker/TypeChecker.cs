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
            SymbolsTree table = new SymbolsTree();
            var inferredFields = new List<(InferredFieldNode, InferredFieldDeclaration)>();
            var classItems = new List<(ClassItemNode, ClassItemDeclaration)>();

            //Built symbols table
            SymbolNode namespaceNode = table.AddNamespace(namespaceDecl);
            foreach (ClassDeclaration classDecl in namespaceDecl.ClassDeclarations)
            {
                ClassNode classNode = table.AddClassNode(classDecl, namespaceNode);
                foreach (ClassItemDeclaration classItem in classDecl.ClassItems)
                {
                    switch (classItem)
                    {
                        case InferredFieldDeclaration iFieldDecl:
                            inferredFields.Add((table.AddInferredFieldNode(iFieldDecl, classNode), iFieldDecl));
                            break;
                        case SimpleFieldDeclaration sFieldDecl:
                            classItems.Add((table.AddSimpleFieldNode(sFieldDecl, classNode), sFieldDecl));
                            break;
                        case MethodDeclaration methodDecl:
                            classItems.Add((table.AddMethodNode(methodDecl, classNode), methodDecl));
                            break;
                        case ConstructorDeclaration ctorDecl:
                            classItems.Add((table.AddConstructorNode(ctorDecl, classNode), ctorDecl));
                            break;
                        default: throw new NotImplementedException();
                    }
                }
            }

            //Resolve inferred field types
            foreach ((InferredFieldNode node, InferredFieldDeclaration decl) in inferredFields)
            {
                var options = CheckExpressionOptions.DisallowInferredFields & CheckExpressionOptions.RequireStatic & CheckExpressionOptions.DisallowDeclarations;
                ValueTypeInfo defaultType = CheckExpression(decl.DefaultValue, node, null, options);
                node.Type = defaultType;
            }

            //Validate other class items
            foreach ((ClassItemNode node, ClassItemDeclaration decl) in classItems)
            {
                switch (decl)
                {
                    case SimpleFieldDeclaration sField:
                        if (sField.DefaultValue != null)
                        {
                            var options = CheckExpressionOptions.RequireStatic & CheckExpressionOptions.DisallowDeclarations;
                            ValueTypeInfo defaultType = CheckExpression(sField.DefaultValue, node, null, options);
                            if (defaultType != ((FieldNode)node).Type) throw new TypeMismatchException();
                        }
                        break;
                    case MethodDeclaration method:
                        {
                            //VerifyFunction(method.ParameterList, method.MethodBody, )
                        }
                        break;
                    case ConstructorDeclaration ctor:
                        {

                        }
                        break;
                    default: throw new NotImplementedException();
                }
            }
            ;
        }

        private static void VerifyFunction(ParameterListDeclaration parameters, MethodBodyDeclaration body, ValueTypeInfo returnType)
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
