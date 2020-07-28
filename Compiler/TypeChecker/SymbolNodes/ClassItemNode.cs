using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.SymbolNodes
{
    public class ClassItemNode : SymbolNode
    {
        public Modifiers Modifiers { get; set; }

        public ClassItemNode(string name, SymbolNode parent, Modifiers modifiers)
            : base(name, parent)
        {
            Modifiers = modifiers;
        }
    }
}
