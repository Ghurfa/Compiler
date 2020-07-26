using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.SymbolNodes
{
    class GlobalNode : NamespaceNode
    {
        public GlobalNode()
            :base("$global", null)
        {

        }
    }
}
