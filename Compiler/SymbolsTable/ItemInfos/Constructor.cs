using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.TypeInfos;
using Parser.SyntaxTreeItems;

namespace SymbolsTable
{
    public class Constructor : ClassMember
    {
        public virtual ValueTypeInfo[] ParamTypes { get; protected set; }
        public ConstructorDeclaration Declaration { get; private set; }

        public Constructor(SymbolsTable table, ConstructorDeclaration ctorDecl)
            : base("$ctor", new Modifiers(ctorDecl.Modifiers))
        {
            Declaration = ctorDecl;
            ParamTypes = new ValueTypeInfo[ctorDecl.ParameterList.Parameters.Length];
            for (int i = 0; i < ParamTypes.Length; i++)
            {
                ParamTypes[i] = ValueTypeInfo.Get(table, ctorDecl.ParameterList.Parameters[i].Type);
            }
        }

        protected Constructor(string name, Modifiers modifiers)
            : base(name, modifiers)
        {
        }
    }
}
