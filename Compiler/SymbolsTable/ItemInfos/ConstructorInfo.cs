using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.TypeInfos;

namespace SymbolsTable
{
    public class ConstructorInfo : ClassMemberInfo
    {
        public ValueTypeInfo[] ParamTypes { get; private set; }
        public ConstructorDeclaration Declaration { get; private set; }

        public ConstructorInfo(SymbolsTable table, ConstructorDeclaration ctorDecl)
            : base("$ctor", new Modifiers(ctorDecl.Modifiers))
        {
            Declaration = ctorDecl;
            ParamTypes = new ValueTypeInfo[ctorDecl.ParameterList.Parameters.Length];
            for (int i = 0; i < ParamTypes.Length; i++)
            {
                ParamTypes[i] = ValueTypeInfo.Get(table, ctorDecl.ParameterList.Parameters[i].Type);
            }
        }
    }
}
