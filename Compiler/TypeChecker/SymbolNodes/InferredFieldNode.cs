using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.SymbolNodes
{
    class InferredFieldNode : FieldNode
    {
        public InferredFieldNode(string name, SymbolNode parent, Modifiers modifiers)
            : base(name, parent, null, modifiers)
        {
        }
    }
}
