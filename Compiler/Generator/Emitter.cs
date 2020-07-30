using Parser;
using Parser.SyntaxTreeItems;
using SymbolsTable;
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
                case ForBlock forBlock:
                    {
                        table.EnterNextScope();

                        Label bodyStart = generator.DefineLabel();
                        Label evalContinue = generator.DefineLabel();

                        //Start
                        EmitStatement(forBlock.StartStatement);
                        generator.Emit(OpCodes.Br, evalContinue);

                        //Body
                        generator.MarkLabel(bodyStart);
                        EmitStatement(forBlock.Body);

                        //Iterate
                        EmitStatement(forBlock.IterateStatement);

                        //Continue check
                        generator.MarkLabel(evalContinue);
                        EmitExpression(forBlock.ContinueExpr);
                        generator.Emit(OpCodes.Brtrue, bodyStart);

                        table.ExitScope();
                    }
                    break;
                case ExpressionStatement exprStatement:
                    switch (exprStatement.Expression)
                    {
                        case DeclAssignExpression declAssign:
                            {
                                string name = ((IdentifierExpression)declAssign.To).Identifier.Text;
                                LocalBuilder local = EmitDeclaration(name);
                                EmitExpression(declAssign.From);
                                generator.Emit(OpCodes.Stloc, local);
                            }
                            break;
                        default:
                            EmitExpression(exprStatement.Expression);
                            break;
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
                case IntLiteralExpression intLiteral:
                    {
                        int value = int.Parse(intLiteral.IntLiteral.Text);
                        switch (value)
                        {
                            case -1: generator.Emit(OpCodes.Ldc_I4_M1); return;
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
                case IdentifierExpression identifier:
                    {
                        string text = identifier.Identifier.Text;
                        Result getFieldRes = table.GetField(text, true, out Field field);
                        switch (getFieldRes)
                        {
                            case Result.Success:
                                generator.Emit(OpCodes.Ldsfld, maps[field]);
                                break;
                            case Result.InvalidInstanceReference:
                                generator.Emit(OpCodes.Ldarg_0);
                                generator.Emit(OpCodes.Ldfld, maps[field]);
                                break;
                            case Result.NoSuchMember:
                                table.GetLocal(text, out Local local);
                                if (local is ParamLocal paramLocal)
                                    generator.Emit(OpCodes.Ldarg, paramLocal.Index);
                                else generator.Emit(OpCodes.Ldloc, maps[local]);
                                break;
                            default: throw new NotImplementedException();
                        }
                    }
                    return;
                case PostIncrementExpression postIncr:
                    switch(postIncr.BaseExpression)
                    {
                        default: throw new NotImplementedException();
                    }
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

        private void EmitExpressionSides(Expression expr)
        {
            EmitExpression(expr.RightExpr);
            EmitExpression(expr.LeftExpr);
        }
    }
}
