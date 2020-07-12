using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class QualifiedIdentifierPart
    {
        public readonly Token Identifier;
        public readonly Token? Dot;
        public QualifiedIdentifierPart(TokenCollection tokens)
        {
            Identifier = tokens.PopToken(TokenType.Identifier);
            if (tokens.PopIfMatches(out Token dotToken, TokenType.Dot))
            {
                Dot = dotToken;
            }
        }
        public QualifiedIdentifierPart(TokenCollection tokens, Token identifier, Token? dot)
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
