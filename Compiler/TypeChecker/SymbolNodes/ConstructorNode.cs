using Compiler.SyntaxTreeItems;
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

        public ConstructorNode(ConstructorDeclaration ctorDecl, ClassNode parent)
            : base("$ctor", parent, new Modifiers(ctorDecl.Modifiers))
        {
            ParamTypes = new ValueTypeInfo[ctorDecl.ParameterList.Parameters.Length];
            for (int i = 0; i < ParamTypes.Length; i++)
            {
                ParamTypes[i] = new ValueTypeInfo(ctorDecl.ParameterList.Parameters[i].Type.ToString());
            }
        }
    }
}
