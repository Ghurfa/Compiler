using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.SymbolNodes
{
    public class GlobalNode : NamespaceNode
    {
        public GlobalNode()
            :base("$global", null)
        {

        }
    }
}
