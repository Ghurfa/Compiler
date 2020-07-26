using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.SymbolNodes
{
    class InferredFieldNode : FieldNode
    {
        public InferredFieldNode(SymbolsTable table, InferredFieldDeclaration iFieldDecl, ClassNode parent)
            : base(table, iFieldDecl.Name.Text, parent, new Modifiers(iFieldDecl.Modifiers), iFieldDecl)
        {
        }
    }
}
