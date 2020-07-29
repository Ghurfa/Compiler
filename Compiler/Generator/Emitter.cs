using Parser;
using Parser.SyntaxTreeItems;
using SymbolsTable;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void EmitMethodBody(ILGenerator generator, Mappings types, SymbolsTable.SymbolsTable table, ParameterListDeclaration paramsList, MethodBodyDeclaration body)
        {
            table.EnterMethod(paramsList);
            EmitStatementSet(generator, types, table, body.Statements);
            table.ExitMethod();
        }

        private static void EmitStatementSet(ILGenerator generator, Mappings types, SymbolsTable.SymbolsTable table, Statement[] statements)
        {
            foreach(Statement statement in statements)
            {
                EmitStatement(generator, types, table, statement);
            }
        }

        private static void EmitStatement(ILGenerator generator, Mappings types, SymbolsTable.SymbolsTable table, Statement statement)
        {
            switch (statement)
            {
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
