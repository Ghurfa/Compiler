using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class QualifiedIdentifierPart
    {
        public readonly IToken Identifier;
        public readonly IToken? Dot;
        public QualifiedIdentifierPart(TokenCollection tokens)
        {
            Identifier = tokens.PopToken(TokenType.Identifier);
            if (tokens.PopIfMatches(out IToken dotToken, TokenType.Dot))
            {
                Dot = dotToken;
            }
        }
        public QualifiedIdentifierPart(TokenCollection tokens, IToken identifier, IToken? dot)
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
