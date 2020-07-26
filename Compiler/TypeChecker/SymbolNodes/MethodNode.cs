using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.TypeInfos;

namespace TypeChecker.SymbolNodes
{
    class MethodNode : ClassItemNode
    {
        public FunctionTypeInfo Type { get; set; }
        public MethodDeclaration Declaration { get; set; }

        public MethodNode(SymbolsTable table, MethodDeclaration methodDecl, ClassNode parent)
            : base(methodDecl.Name.Text, parent, new Modifiers(methodDecl.Modifiers))
        {
            TypeInfo retType;
            if (methodDecl.ReturnType is VoidType) retType = VoidTypeInfo.Get();
            else retType = ValueTypeInfo.Get(table, methodDecl.ReturnType);

            ValueTypeInfo[] paramTypes = new ValueTypeInfo[methodDecl.ParameterList.Parameters.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                paramTypes[i] = ValueTypeInfo.Get(table, methodDecl.ParameterList.Parameters[i].Type);
            }

            Declaration = methodDecl;
            Type = new FunctionTypeInfo(retType, paramTypes);
        }
    }
}
