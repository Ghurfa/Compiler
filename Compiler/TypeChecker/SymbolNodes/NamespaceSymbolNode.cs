using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    class NamespaceSymbolNode : SymbolNode
    {
        public NamespaceSymbolNode(string name, SymbolNode parent)
            :base(name, parent)
        {

        }
    }
}
