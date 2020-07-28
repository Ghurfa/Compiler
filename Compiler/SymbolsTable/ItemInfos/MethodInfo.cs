using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.TypeInfos;

namespace SymbolsTable
{
    public class MethodInfo : ClassMemberInfo
    {
        public FunctionTypeInfo Type { get; private set; }
        public MethodDeclaration Declaration { get; private set; }

        public MethodInfo(SymbolsTable table, MethodDeclaration methodDecl)
            : base(methodDecl.Name.Text, new Modifiers(methodDecl.Modifiers))
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

        internal MethodInfo(string name, FunctionTypeInfo type, Modifiers modifiers)
            :base(name, modifiers)
        {
            Type = type;
        }
    }
}
