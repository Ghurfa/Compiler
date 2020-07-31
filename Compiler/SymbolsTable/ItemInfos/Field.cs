using Parser;
using Parser.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.TypeInfos;

namespace SymbolsTable
{
    public class Field : ClassMember
    {
        private ValueTypeInfo type;
        public virtual ValueTypeInfo Type
        {
            get => type; 
            set
            {
                if (type == null) type = value;
                else throw new InvalidOperationException();
            }
        }

        protected Field(string name, Modifiers modifiers)
            : base(name, modifiers)
        {
            Type = null;
        }

        public Field(SymbolsTable table, Parser.SyntaxTreeItems.Type type, string name, Modifiers modifiers)
            : base(name, modifiers)
        {
            Type = ValueTypeInfo.Get(table, type);
        }
    }
}
