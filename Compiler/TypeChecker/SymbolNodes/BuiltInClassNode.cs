using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.SymbolNodes
{
    class BuiltInClassNode : ClassNode
    {
        public BuiltInClassNode(string name, GlobalNode parent)
            :base(name, null, parent, Modifiers.Public, null)
        {
        }
    }
}
