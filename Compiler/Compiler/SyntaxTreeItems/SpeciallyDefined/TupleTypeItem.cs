using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class TupleTypeItem
    {
        public readonly IdentifierToken? Name;
        public readonly ColonToken? Colon;
        public readonly Type Type;
        public readonly CommaToken? Comma;

        public TupleTypeItem(TokenCollection tokens)
        {
            if (tokens.PopIfMatches(out Name))
            {
                if (tokens.PopIfMatches(out Colon))
                {
                    Type = Type.ReadType(tokens);
                }
                else
                {
                    Type = new QualifiedIdentifierType(tokens, new QualifiedIdentifier(tokens, new QualifiedIdentifierPart[] { new QualifiedIdentifierPart(tokens, Name) }));
                }
            }
            else
            {
                Type = Type.ReadType(tokens);
            }
            tokens.PopIfMatches(out Comma);
        }
        public override string ToString()
        {
            return Name?.ToString() + Colon?.ToString() + Type.ToString() + Comma.ToString();
        }
    }
}
