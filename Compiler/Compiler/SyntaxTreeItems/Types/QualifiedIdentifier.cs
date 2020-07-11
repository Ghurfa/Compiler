using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems.Types
{
    public class QualifiedIdentifier : Type
    {
        public readonly QualifiedIdentifierPart[] Parts;
        public QualifiedIdentifier(TokenCollection tokens)
        {
            var parts = new LinkedList<QualifiedIdentifierPart>();
            bool lastMissingDot = false;
            while (!lastMissingDot)
            {
                var newPart = new QualifiedIdentifierPart(tokens);
                parts.AddLast(newPart);
                lastMissingDot = newPart.Dot == null;
            }
            Parts = parts.ToArray();
        }
        public QualifiedIdentifier(TokenCollection tokens, Token firstIdentifier)
        {
            if (tokens.PopIfMatches(out Token firstDot, TokenType.Dot))
            {
                var parts = new LinkedList<QualifiedIdentifierPart>();
                var newPart = new QualifiedIdentifierPart(tokens, firstIdentifier, firstDot);
                parts.AddLast(newPart);
                bool lastMissingDot = newPart.Dot == null;

                while (!lastMissingDot)
                {
                    newPart = new QualifiedIdentifierPart(tokens);
                    parts.AddLast(newPart);
                    lastMissingDot = newPart.Dot == null;
                }
                Parts = parts.ToArray();
            }
            else
            {
                Parts = new QualifiedIdentifierPart[] { new QualifiedIdentifierPart(tokens, firstIdentifier, null) };
            }
        }
    }
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
    }
}
