using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.SymbolNodes
{
    class ClassItemNode : SymbolNode
    {
        public Modifiers Modifiers { get; set; }

        public ClassItemNode(string name, SymbolNode parent, Modifiers modifiers)
            : base(name, parent)
        {
            Modifiers = modifiers;
        }
    }
}
