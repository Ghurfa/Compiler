using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.SymbolNodes
{
    class InferredFieldNode : FieldNode
    {

        public InferredFieldNode(InferredFieldDeclaration iFieldDecl, ClassNode parent)
            : base(iFieldDecl.Name.Text, parent, null, new Modifiers(iFieldDecl.Modifiers), iFieldDecl)
        {
        }
    }
}
