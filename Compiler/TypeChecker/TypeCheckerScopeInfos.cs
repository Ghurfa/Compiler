using Parser.SyntaxTreeItems;
using SymbolsTable;
using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker
{
    public static partial class TypeChecker
    {
        [Flags]
        private enum VerifyConstraints
        {
            RequireStatic = 1,
            DisallowReferences = 2,
            DisallowDeclarations = 4,
            DisallowScoping = 8,
        }

        private interface IScopeInfo
        {
            int IndexInParent { get; }
            void Verify(SymbolsTableBuilder table, TypeInfo returnType, VerifyConstraints constraints);
        }

        private struct NormalScopeInfo : IScopeInfo
        {
            public int IndexInParent { get; private set; }
            private Statement[] statements;

            public NormalScopeInfo(Statement[] statements, int index)
            {
                this.statements = statements;
                IndexInParent = index;
            }

            public void Verify(SymbolsTableBuilder table, TypeInfo returnType, VerifyConstraints constraints)
            {
                table.EnterNewScope(IndexInParent);

                List<IScopeInfo> childScopes = new List<IScopeInfo>();
                for (int i = 0; i < statements.Length; i++)
                {
                    VerifyStatement(table, i, childScopes, statements[i], returnType, constraints);
                }

                foreach (IScopeInfo childScope in childScopes)
                {
                    childScope.Verify(table, returnType, constraints);
                }

                table.ExitScope();
            }
        }

        private struct ForScopeInfo : IScopeInfo
        {
            public int IndexInParent { get; private set; }
            private ForBlock forBlock;

            public ForScopeInfo(ForBlock forBlock, int index)
            {
                this.forBlock = forBlock;
                IndexInParent = index;
            }

            public void Verify(SymbolsTableBuilder table, TypeInfo returnType, VerifyConstraints constraints)
            {
                VerifyConstraints startConstraints = constraints | VerifyConstraints.DisallowScoping;
                VerifyConstraints otherConstraints = constraints | VerifyConstraints.DisallowScoping | VerifyConstraints.DisallowDeclarations;

                table.EnterNewScope(IndexInParent);
                VerifyStatement(table, 0, null, forBlock.StartStatement, returnType, startConstraints);
                VerifyExpressionRequireType(table, 1, forBlock.ContinueExpr, otherConstraints, ValueTypeInfo.PrimitiveTypes["bool"]);
                VerifyStatement(table, 2, null, forBlock.IterateStatement, returnType, otherConstraints);

                var bodyScopes = new List<IScopeInfo>();
                VerifyStatement(table, 3, bodyScopes, forBlock.Body, returnType, constraints);
                foreach (IScopeInfo childScope in bodyScopes) childScope.Verify(table, returnType, constraints);

                table.ExitScope();
            }
        }

    }
}
