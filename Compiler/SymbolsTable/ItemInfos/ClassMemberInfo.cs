using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class ClassMemberInfo
    {
        public string Name { get; protected set; }
        public Modifiers Modifiers { get; protected set; }

        private ClassMemberInfo() { }

        protected ClassMemberInfo(string name, Modifiers modifiers)
        {
            Name = name;
            Modifiers = modifiers;
        }
    }
}
