using Compiler;
using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.TypeInfos;

namespace SymbolsTable
{
    public class FieldInfo : ClassMemberInfo
    {
        private ValueTypeInfo type;
        public ValueTypeInfo Type
        {
            get => type; 
            set
            {
                if (type == null) type = value;
                else throw new InvalidOperationException();
            }
        }

        protected FieldInfo(string name, Modifiers modifiers)
            : base(name, modifiers)
        {
            Type = null;
        }

        public FieldInfo(SymbolsTable table, Compiler.SyntaxTreeItems.Type type, string name, Modifiers modifiers)
            : base(name, modifiers)
        {
            Type = ValueTypeInfo.Get(table, type);
        }
    }
}
