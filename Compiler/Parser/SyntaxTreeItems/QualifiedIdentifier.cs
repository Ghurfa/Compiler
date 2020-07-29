using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
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
            var parts = new LinkedList<QualifiedIdentifierPart>();

            QualifiedIdentifierPart newPart = new QualifiedIdentifierPart(tokens, firstIdentifier);

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
        public override string ToString()
        {
            string ret = "";
            for (int i = 0; i < Parts.Length; i++)
            {
                ret += Parts[i];
                if (i < Parts.Length - 1) ret += " ";
            }
            return ret;
        }
    }
}
