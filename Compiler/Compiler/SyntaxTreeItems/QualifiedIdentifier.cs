using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems.Types
{
    public class QualifiedIdentifier
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
        public QualifiedIdentifier(TokenCollection tokens, IdentifierToken firstIdentifier)
        {
            if (tokens.PopIfMatches(out DotToken firstDot))
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
        public override string ToString()
        {
            string ret = "";
            for(int i = 0; i < Parts.Length; i++)
            {
                ret += Parts[i];
                if (i < Parts.Length - 1) ret += " ";
            }
            return ret;
        }
    }
}
