using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.TypeInfos;

namespace TypeChecker.SymbolNodes
{
    class FieldNode : ClassItemNode
    {
        public TypeInfo Type { get; set; }

        public FieldNode(string name, SymbolNode parent, TypeInfo type, Modifiers modifiers)
            : base(name, parent, modifiers)
        {
            Type = type;
        }

        public FieldNode(SimpleFieldDeclaration sFieldDecl, ClassNode parent)
            : base(sFieldDecl.Name.Text, parent, new Modifiers(sFieldDecl.Modifiers))
        {
            Type = new ValueTypeInfo(sFieldDecl.Type.ToString());
        }
    }
}
