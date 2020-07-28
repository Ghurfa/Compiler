using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    public class NamespaceNode : SymbolNode
    {
        public NamespaceNode(string name, SymbolNode parent)
            :base(name, parent)
        {

        }
    }
}
