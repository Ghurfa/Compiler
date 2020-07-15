using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class QualifiedIdentifierPart
    {
        public readonly IdentifierToken Identifier;
        public readonly DotToken? Dot;
        public QualifiedIdentifierPart(TokenCollection tokens)
        {
            Identifier = tokens.PopToken<IdentifierToken>();
            if (tokens.PopIfMatches(out DotToken dotToken))
            {
                Dot = dotToken;
            }
        }
        public QualifiedIdentifierPart(TokenCollection tokens, IdentifierToken identifier, DotToken? dot)
        {
            Identifier = identifier;
            Dot = dot;
        }
        public override string ToString()
        {
            return Identifier.ToString() + Dot?.ToString();
        }
    }
}
