using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class InferredFieldInfo : FieldInfo
    {
        public InferredFieldDeclaration Declaration { get; private set; }

        public InferredFieldInfo(InferredFieldDeclaration iFieldDecl)
            : base(iFieldDecl.Name.Text, new Modifiers(iFieldDecl.Modifiers))
        {
            Declaration = iFieldDecl;
        }
    }
}
