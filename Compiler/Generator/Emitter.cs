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
    static class Emitter
    {
        private static ConstructorInfo objectCtor = typeof(object).GetConstructor(new System.Type[0]);
        public static void EmitConstructorStart(ILGenerator generator, ClassBuildingInfo info)
        {
            foreach ((FieldBuilder field, Expression value) in info.DefaultedFields)
            {
                generator.Emit(OpCodes.Ldarg_0);
                EmitExpression(generator, value);
                generator.Emit(OpCodes.Stfld, field);
            }
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Newobj, objectCtor);
        }

        public static void EmitFunctionBody(ILGenerator generator, Mappings maps, SymbolsTable.SymbolsTable table, Constructor ctor)
        {
            table.EnterFunction(ctor);
            Emitter.EmitStatementSet(generator, maps, table, ctor.Declaration.Body.Statements);
            table.ExitFunction();
        }

        public static void EmitFunctionBody(ILGenerator generator, Mappings maps, SymbolsTable.SymbolsTable table, Method method)
        {
            table.EnterFunction(method);
            Emitter.EmitStatementSet(generator, maps, table, method.Declaration.Body.Statements);
            table.ExitFunction();
        }

        private static void EmitStatementSet(ILGenerator generator, Mappings maps, SymbolsTable.SymbolsTable table, Statement[] statements)
        {
            foreach (Statement statement in statements)
            {
                EmitStatement(generator, maps, table, statement);
            }
        }

        private static void EmitStatement(ILGenerator generator, Mappings maps, SymbolsTable.SymbolsTable table, Statement statement)
        {
            switch (statement)
            {
                case CodeBlock codeBlock:
                    {
                        table.EnterScope();
                        EmitStatementSet(generator, maps, table, codeBlock.Statements);
                        table.ExitScope();
                    }
                    break;
                case ForBlock forBlock:
                    {
                        table.EnterScope();

                        Label bodyStart = generator.DefineLabel();
                        Label evalContinue = generator.DefineLabel();

                        //Start
                        EmitStatement(generator, maps, table, forBlock.StartStatement);
                        generator.Emit(OpCodes.Br, evalContinue);

                        //Body
                        generator.MarkLabel(bodyStart);
                        EmitStatement(generator, maps, table, forBlock.Body);

                        //Iterate
                        EmitStatement(generator, maps, table, forBlock.IterateStatement);

                        //Continue check
                        generator.MarkLabel(evalContinue);
                        EmitExpression(generator, forBlock.ContinueExpr);
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
                                //var local = generator.DeclareLocal()
                            }
                            break;
                    }
                    break;
                default: throw new NotImplementedException();
            }
        }

        public static void EmitExpression(ILGenerator generator, Expression expr)
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
                            default:
                                if (value >= short.MinValue && value <= short.MaxValue)
                                    generator.Emit(OpCodes.Ldc_I4_S, (short)value);
                                else
                                    generator.Emit(OpCodes.Ldc_I4, value);
                                return;
                        }
                    }
                default: throw new NotImplementedException();
            }
        }
    }
}
