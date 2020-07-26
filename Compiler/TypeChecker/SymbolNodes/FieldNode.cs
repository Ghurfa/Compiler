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
        public ValueTypeInfo Type { get; set; }
        public FieldDeclaration Declaration { get; set; }

        protected FieldNode(SymbolsTable table, string name, SymbolNode parent, Modifiers modifiers, FieldDeclaration declaration)
            : base(name, parent, modifiers)
        {
            Type = null;
            Declaration = declaration;
        }

        public FieldNode(SymbolsTable table, SimpleFieldDeclaration sFieldDecl, ClassNode parent)
            : base(sFieldDecl.Name.Text, parent, new Modifiers(sFieldDecl.Modifiers))
        {
            Type = ValueTypeInfo.Get(table, sFieldDecl.Type);
            Declaration = sFieldDecl;
        }
    }
}
