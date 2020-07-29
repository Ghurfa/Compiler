using Parser.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class SimpleField : Field
    {
        public SimpleFieldDeclaration Declaration { get; private set; }

        public SimpleField(SymbolsTable table, SimpleFieldDeclaration sFieldDecl)
            : base(table, sFieldDecl.Type, sFieldDecl.Name.Text, new Modifiers(sFieldDecl.Modifiers))
        {
            Declaration = sFieldDecl;
        }
    }
}
