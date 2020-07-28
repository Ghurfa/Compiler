using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class SimpleFieldInfo : FieldInfo
    {
        public SimpleFieldDeclaration Declaration { get; private set; }

        public SimpleFieldInfo(SymbolsTable table, SimpleFieldDeclaration sFieldDecl)
            : base(table, sFieldDecl.Type, sFieldDecl.Name.Text, new Modifiers(sFieldDecl.Modifiers))
        {
            Declaration = sFieldDecl;
        }
    }
}
