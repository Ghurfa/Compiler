using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable.Nodes
{
    public class BuiltInClassNode : ClassNode
    {
        public BuiltInClassNode(string name, ClassNode parentClass, GlobalNode parent)
            :base(name, null, parentClass, parent, Modifiers.Public, null)
        {
        }
    }
}
