using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class ClassMember
    {
        public string Name { get; protected set; }
        public Modifiers Modifiers { get; protected set; }

        private ClassMember() { }

        protected ClassMember(string name, Modifiers modifiers)
        {
            Name = name;
            Modifiers = modifiers;
        }
    }
}
