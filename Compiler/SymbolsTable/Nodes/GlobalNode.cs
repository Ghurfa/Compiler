using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable.Nodes
{
    public class GlobalNode : NamespaceNode
    {
        public GlobalNode()
            :base("$global", null)
        {

        }
    }
}
