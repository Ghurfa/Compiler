﻿using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.TypeInfos;

namespace TypeChecker.SymbolNodes
{
    public class ConstructorNode : ClassItemNode
    {
        public ValueTypeInfo[] ParamTypes { get; set; }
        public ConstructorDeclaration Declaration { get; set; }

        public ConstructorNode(SymbolsTable table, ConstructorDeclaration ctorDecl, ClassNode parent)
            : base("$ctor", parent, new Modifiers(ctorDecl.Modifiers))
        {
            Declaration = ctorDecl;
            ParamTypes = new ValueTypeInfo[ctorDecl.ParameterList.Parameters.Length];
            for (int i = 0; i < ParamTypes.Length; i++)
            {
                ParamTypes[i] = ValueTypeInfo.Get(table, ctorDecl.ParameterList.Parameters[i].Type);
            }
        }
    }
}
