using Parser.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class InferredField : Field
    {
        public InferredFieldDeclaration Declaration { get; private set; }

        public InferredField(InferredFieldDeclaration iFieldDecl)
            : base(iFieldDecl.Name.Text, new Modifiers(iFieldDecl.Modifiers))
        {
            Declaration = iFieldDecl;
        }
    }
}
