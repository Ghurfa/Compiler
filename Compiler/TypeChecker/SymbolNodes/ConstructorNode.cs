using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.TypeInfos;

namespace TypeChecker.SymbolNodes
{
    class ConstructorNode : ClassItemNode
    {
        public ValueTypeInfo[] ParamTypes { get; set; }
        public ConstructorNode(ClassNode parent, ValueTypeInfo[] paramTypes, Modifiers modifiers)
            : base("$ctor", parent, modifiers)
        {
            ParamTypes = paramTypes;
        }
    }
}
