using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.SymbolNodes
{
    class BuiltInClassNode : ClassNode
    {
        public BuiltInClassNode(string name, ClassNode parentClass, GlobalNode parent)
            :base(name, null, parentClass, parent, Modifiers.Public, null)
        {
        }
    }
}
