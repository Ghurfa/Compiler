using System;
using System.Collections.Generic;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class TupleTypeItem
    {
        public readonly IdentifierToken? Name;
        public readonly ColonToken? Colon;
        public readonly Type Type;
        public readonly CommaToken? Comma;

        public TupleTypeItem(TokenCollection tokens)
        {
            if (tokens.PopIfMatches(out IdentifierToken identifier))
            {
                if (tokens.PopIfMatches(out ColonToken colon))
                {
                    Name = identifier;
                    Colon = colon;
                    Type = Type.ReadType(tokens);
                }
                else
                {
                    Type = new QualifiedIdentifierType(tokens, new QualifiedIdentifier(tokens, identifier));
                }
            }
            else
            {
                Type = Type.ReadType(tokens);
            }
            if (tokens.PopIfMatches(out CommaToken comma))
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
