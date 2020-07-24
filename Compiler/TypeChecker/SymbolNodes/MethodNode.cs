using Compiler.SyntaxTreeItems;
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

        public MethodNode(MethodDeclaration methodDecl, ClassNode parent)
            :base(methodDecl.Name.Text, parent, new Modifiers(methodDecl.Modifiers))
        {
            ValueTypeInfo retType = new ValueTypeInfo(methodDecl.ReturnType.ToString());
            ValueTypeInfo[] paramTypes = new ValueTypeInfo[methodDecl.ParameterList.Parameters.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                paramTypes[i] = new ValueTypeInfo(methodDecl.ParameterList.Parameters[i].Type.ToString());
            }

            Type = new FunctionTypeInfo(retType, paramTypes);
        }
    }   
}
