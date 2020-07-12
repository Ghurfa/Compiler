using Compiler.SyntaxTreeItems.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class TupleTypeItem
    {
        public readonly Token? Name;
        public readonly Token? Colon;
        public readonly Type Type;
        public readonly Token? Comma;

        public TupleTypeItem(TokenCollection tokens)
        {
            if (tokens.PopIfMatches(out Token identifier, TokenType.Identifier))
            {
                if (tokens.PopIfMatches(out Token colon, TokenType.Colon))
                {
                    Name = identifier;
                    Colon = colon;
                    Type = Type.ReadType(tokens);
                }
                else
                {
                    Type = new QualifiedIdentifierType(tokens, identifier);
                }
            }
            else
            {
                Type = Type.ReadType(tokens);
            }
            if (tokens.PopIfMatches(out Token comma, TokenType.Comma))
            {
                Comma = comma;
            }
        }
        public override string ToString()
        {
            return Name?.ToString() + Colon?.ToString() + Type.ToString() + Comma.ToString();
        }
    }
}
