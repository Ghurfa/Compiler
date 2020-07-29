using Parser.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using Tokenizer;

namespace Parser
{
    public abstract class FieldDeclaration : ClassItemDeclaration
    {
        public IdentifierToken Name { get; protected set; }
        public ModifierList Modifiers { get; protected set; }

        public SemicolonToken? Semicolon { get; protected set; }
        public FieldDeclaration(TokenCollection tokens, IdentifierToken name)
        {
            Name = name;
        }
    }
}
