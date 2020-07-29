using Parser.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SymbolsTable.TypeInfos;

namespace SymbolsTable
{
    public class NamespaceNode : SymbolNode
    {
        public NamespaceNode(string name, SymbolNode parent)
            :base(name, parent)
        {

        }
    }
}
