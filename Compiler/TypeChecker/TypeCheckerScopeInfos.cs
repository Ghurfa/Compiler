using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.TypeInfos;

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
            void Verify(SymbolsTable table, TypeInfo returnType, VerifyConstraints constraints);
        }

        private struct FunctionScopeInfo : IScopeInfo
        {
            public int IndexInParent { get; private set; }
            private ParameterListDeclaration parameterList;
            private Statement[] statements;

            public FunctionScopeInfo(ParameterListDeclaration parameters, Statement[] body)
            {
                parameterList = parameters;
                statements = body;
                IndexInParent = 1;
            }

            public void Verify(SymbolsTable table, TypeInfo returnType, VerifyConstraints constraints)
            {
                table.EnterMethod(parameterList);

                List<IScopeInfo> childScopes = new List<IScopeInfo>();
                for (int i = 0; i < statements.Length; i++)
                {
                    VerifyStatement(table, i, childScopes, statements[i], returnType, constraints);
                }

                foreach (IScopeInfo childScope in childScopes)
                {
                    childScope.Verify(table, returnType, constraints);
                }

                table.ExitMethod();
            }
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

            public void Verify(SymbolsTable table, TypeInfo returnType, VerifyConstraints constraints)
            {
                table.EnterScope(IndexInParent);

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

            public void Verify(SymbolsTable table, TypeInfo returnType, VerifyConstraints constraints)
            {
                VerifyConstraints startConstraints = constraints | VerifyConstraints.DisallowScoping;
                VerifyConstraints otherConstraints = constraints | VerifyConstraints.DisallowScoping | VerifyConstraints.DisallowDeclarations;

                table.EnterScope(IndexInParent);
                VerifyStatement(table, 0, null, forBlock.StartStatement, returnType, constraints);
                VerifyExpressionRequireType(table, 1, forBlock.ContinueExpr, constraints | VerifyConstraints.DisallowDeclarations, ValueTypeInfo.PrimitiveTypes["bool"]);
                VerifyStatement(table, 2, null, forBlock.IterateStatement, returnType, constraints | VerifyConstraints.DisallowDeclarations);

                var bodyScopes = new List<IScopeInfo>();
                VerifyStatement(table, 3, bodyScopes, forBlock.Body, returnType, constraints);
                foreach (IScopeInfo childScope in bodyScopes) childScope.Verify(table, returnType, constraints);

                table.ExitScope();
            }
        }

    }
}
