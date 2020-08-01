using Parser;
using Parser.SyntaxTreeItems;
using Parser.SyntaxTreeItems.Statements;
using SymbolsTable;
using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    class Emitter
    {
        private ILGenerator generator;
        private Mappings maps;
        SymbolsTable.SymbolsTable table;

        public Emitter(Mappings maps, SymbolsTable.SymbolsTable table)
        {
            this.maps = maps;
            this.table = table;
        }

        public void SetGenerator(ILGenerator generator) => this.generator = generator;

        private ConstructorInfo objectCtor = typeof(object).GetConstructor(new System.Type[0]);
        public void EmitConstructorStart(ClassBuildingInfo info)
        {
            foreach ((FieldBuilder field, Expression value) in info.DefaultedFields)
            {
                generator.Emit(OpCodes.Ldarg_0);
                EmitExpression(value);
                generator.Emit(OpCodes.Stfld, field);
            }
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Newobj, objectCtor);
        }

        public void EmitFunctionBody(Constructor ctor)
        {
            table.EnterFunction(ctor);
            EmitStatementSet(ctor.Declaration.Body.Statements);
            generator.Emit(OpCodes.Ret);
            table.ExitFunction();
        }

        public void EmitFunctionBody(Method method)
        {
            table.EnterFunction(method);
            EmitStatementSet(method.Declaration.Body.Statements);
            generator.Emit(OpCodes.Ret);
            table.ExitFunction();
        }

        private void EmitStatementSet(Statement[] statements)
        {
            foreach (Statement statement in statements)
            {
                EmitStatement(statement);
            }
        }

        private void EmitStatement(Statement statement)
        {
            switch (statement)
            {
                case CodeBlock codeBlock:
                    {
                        table.EnterNextScope();
                        EmitStatementSet(codeBlock.Statements);
                        table.ExitScope();
                    }
                    break;
                case IfBlock ifBlock:
                    {
                        if (ifBlock.IfFalse == null)
                        {
                            Label ifFalse = generator.DefineLabel();

                            EmitExpression(ifBlock.Condition);
                            generator.Emit(OpCodes.Brfalse, ifFalse);

                            EmitStatement(ifBlock.IfTrue);
                            generator.MarkLabel(ifFalse);
                        }
                        else
                        {
                            Label ifFalse = generator.DefineLabel();
                            Label end = generator.DefineLabel();

                            EmitExpression(ifBlock.Condition);
                            generator.Emit(OpCodes.Brfalse, ifFalse);

                            EmitStatement(ifBlock.IfTrue);
                            generator.Emit(OpCodes.Br, end);

                            generator.MarkLabel(ifFalse);
                            EmitStatement(ifBlock.IfFalse);

                            generator.MarkLabel(end);
                        }
                    }
                    break;
                case ForBlock forBlock:
                    {
                        table.EnterNextScope();

                        Label body = generator.DefineLabel();
                        Label evalContinue = generator.DefineLabel();

                        //Start
                        EmitStatement(forBlock.StartStatement);
                        generator.Emit(OpCodes.Br, evalContinue);

                        //Body
                        generator.MarkLabel(body);
                        EmitStatement(forBlock.Body);

                        //Iterate
                        EmitStatement(forBlock.IterateStatement);

                        //Continue check
                        generator.MarkLabel(evalContinue);
                        EmitExpression(forBlock.ContinueExpr);
                        generator.Emit(OpCodes.Brtrue, body);

                        table.ExitScope();
                    }
                    break;
                case WhileBlock whileBlock:
                    {
                        Label body = generator.DefineLabel();
                        Label evalContinue = generator.DefineLabel();

                        generator.Emit(OpCodes.Br, evalContinue);

                        //Body
                        generator.MarkLabel(body);
                        EmitStatement(whileBlock.Body);

                        //Continue check
                        generator.MarkLabel(evalContinue);
                        EmitExpression(whileBlock.Condition);
                        generator.Emit(OpCodes.Brtrue, body);
                    }
                    break;
                case ExpressionStatement exprStatement:
                    EmitExpression(exprStatement.Expression);
                    Expression expr = exprStatement.Expression;
                    if (expr is MethodCallExpression methodCall)
                    {
                        table.GetMethod(methodCall, out Method method);
                        if (!(method.Type.ReturnType is VoidTypeInfo)) generator.Emit(OpCodes.Pop);
                    }
                    else if (expr is NewObjectExpression ||
                             expr is AssignExpression ||
                             expr is DeclAssignExpression)
                    {
                        generator.Emit(OpCodes.Pop);
                    }
                    break;
                default: throw new NotImplementedException();
            }
        }

        public LocalBuilder EmitDeclaration(string name)
        {
            table.GetLocal(name, out Local local);
            var type = maps[local.Type.Class];
            LocalBuilder builder = generator.DeclareLocal(type);
            maps.MapLocal(local, builder);
            return builder;
        }

        public void EmitExpression(Expression expr)
        {
            switch (expr)
            {
                case PerenthesizedExpression perenthesized: EmitExpression(perenthesized.Expression); return;
                case IntLiteralExpression intLiteral:
                    {
                        int value = int.Parse(intLiteral.IntLiteral.Text);
                        switch (value)
                        {
                            case 0: generator.Emit(OpCodes.Ldc_I4_0); return;
                            case 1: generator.Emit(OpCodes.Ldc_I4_1); return;
                            case 2: generator.Emit(OpCodes.Ldc_I4_2); return;
                            case 3: generator.Emit(OpCodes.Ldc_I4_3); return;
                            case 4: generator.Emit(OpCodes.Ldc_I4_4); return;
                            case 5: generator.Emit(OpCodes.Ldc_I4_5); return;
                            case 6: generator.Emit(OpCodes.Ldc_I4_6); return;
                            case 7: generator.Emit(OpCodes.Ldc_I4_7); return;
                            case 8: generator.Emit(OpCodes.Ldc_I4_8); return;
                            default: generator.Emit(OpCodes.Ldc_I4, value); return;
                        }
                    }
                case TrueLiteralExpression _: generator.Emit(OpCodes.Ldc_I4_1); return;
                case FalseLiteralExpression _: generator.Emit(OpCodes.Ldc_I4_0); return;
                case StringLiteralExpression strLit: generator.Emit(OpCodes.Ldstr, strLit.Text.Text); return;
                case CharLiteralExpression charLit: generator.Emit(OpCodes.Ldstr, charLit.Text.Text); return;
                case IdentifierExpression identifier:
                    {
                        string text = identifier.Identifier.Text;
                        if (table.GetLocal(text, out Local local) == Result.Success)
                        {
                            if (local is ParamLocal paramLocal)
                                generator.Emit(OpCodes.Ldarg, paramLocal.Index);
                            else generator.Emit(OpCodes.Ldloc, maps[local]);
                        }
                        else
                        {
                            Result getFieldRes = table.GetField(identifier, out Field field);
                            switch (getFieldRes)
                            {
                                case Result.Success:
                                    generator.Emit(OpCodes.Ldsfld, maps[field]);
                                    break;
                                case Result.InvalidInstanceReference:
                                    generator.Emit(OpCodes.Ldarg_0);
                                    generator.Emit(OpCodes.Ldfld, maps[field]);
                                    break;
                                default: throw new NotImplementedException();
                            }
                        }
                    }
                    return;
                case NewObjectExpression newObj:
                    {
                        table.GetConstructor(newObj, out Constructor ctor);
                        generator.Emit(OpCodes.Newobj, maps[ctor]);
                    }
                    return;
                case MethodCallExpression methodCall:
                    {
                        table.GetMethod(methodCall, out Method method);
                        if(!method.Modifiers.IsStatic)
                        generator.Emit(OpCodes.Call, maps[method]);
                    }
                    return;

                //Unary operators
                case LogicalNotExpression logicalNot: 
                    EmitExpression(logicalNot.BaseExpression);
                    generator.Emit(OpCodes.Ldc_I4_0);
                    generator.Emit(OpCodes.Ceq);
                    return;
                case UnaryPlusExpression unaryPlus: EmitExpression(unaryPlus.BaseExpression); return;
                case UnaryMinusExpression unaryMinus:
                    EmitExpression(unaryMinus.BaseExpression);
                    generator.Emit(OpCodes.Neg);
                    return;

                //Assignment expressions
                case AssignExpression assign:
                    {
                        EmitExpression(assign.From);
                        generator.Emit(OpCodes.Dup);
                        Assign(assign.To);
                    }
                    return;
                case DeclAssignExpression declAssign:
                    {
                        string name = ((IdentifierExpression)declAssign.To).Identifier.Text;
                        LocalBuilder local = EmitDeclaration(name);
                        EmitExpression(declAssign.From);
                        generator.Emit(OpCodes.Dup);
                        generator.Emit(OpCodes.Stloc, local);
                    }
                    return;

                //Binary operators
                case PlusExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Add); return;
                case MinusExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Sub); return;
                case MultiplyExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Mul); return;
                case DivideExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Div); return;
                case ModuloExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Rem); return;
                case LeftShiftExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Shl); return;
                case RightShiftExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Shr); return;
                case BitwiseAndExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.And); return;
                case BitwiseOrExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Or); return;
                case BitwiseXorExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Xor); return;
                case LessThanExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Clt); return;
                case GreaterThanExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Cgt); return;
                case LessThanOrEqualToExpression _:
                    EmitExpressionSides(expr);
                    generator.Emit(OpCodes.Cgt);
                    generator.Emit(OpCodes.Ldc_I4_0);
                    generator.Emit(OpCodes.Ceq);
                    return;
                case GreaterThanOrEqualToExpression _:
                    EmitExpressionSides(expr);
                    generator.Emit(OpCodes.Clt);
                    generator.Emit(OpCodes.Ldc_I4_0);
                    generator.Emit(OpCodes.Ceq);
                    return;
                case EqualsExpression _: EmitExpressionSides(expr); generator.Emit(OpCodes.Ceq); return;
                case NotEqualsExpression _:
                    EmitExpressionSides(expr);
                    generator.Emit(OpCodes.Ceq);
                    generator.Emit(OpCodes.Ldc_I4_0);
                    generator.Emit(OpCodes.Ceq);
                    return;
                default: throw new NotImplementedException();
            }
        }

        private void Assign(Expression to)
        {
            if (to is IdentifierExpression idExpr && table.GetLocal(idExpr.Identifier.Text, out Local local) == Result.Success)
            {
                if (local is ParamLocal paramLocal)
                    generator.Emit(OpCodes.Starg, paramLocal.Index);
                else generator.Emit(OpCodes.Stloc, maps[local]);
                return;
            }
            table.GetField((PrimaryExpression)to, out Field field);
            if (field.Modifiers.IsStatic)
                generator.Emit(OpCodes.Stsfld);
            else generator.Emit(OpCodes.Stfld);
        }

        private void EmitExpressionSides(Expression expr)
        {
            EmitExpression(expr.RightExpr);
            EmitExpression(expr.LeftExpr);
        }
    }
}
