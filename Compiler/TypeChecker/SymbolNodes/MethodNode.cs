using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.TypeInfos;

namespace TypeChecker.SymbolNodes
{
    class MethodNode : ClassItemNode
    {
        public FunctionTypeInfo Type { get; set; }

        public MethodNode(string name, SymbolNode parent, FunctionTypeInfo type, Modifiers modifiers)
            : base(name, parent, modifiers)
        {
            Type = type;
        }
    }   
}
