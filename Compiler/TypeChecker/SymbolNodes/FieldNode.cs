using Compiler;
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
        public FieldDeclaration Declaration { get; set; }

        protected FieldNode(string name, SymbolNode parent, TypeInfo type, Modifiers modifiers, FieldDeclaration declaration)
            : base(name, parent, modifiers)
        {
            Type = type;
            Declaration = declaration;
        }

        public FieldNode(SimpleFieldDeclaration sFieldDecl, ClassNode parent)
            : base(sFieldDecl.Name.Text, parent, new Modifiers(sFieldDecl.Modifiers))
        {
            Type = ValueTypeInfo.Get(sFieldDecl.Type);
            Declaration = sFieldDecl;
        }
    }
}
