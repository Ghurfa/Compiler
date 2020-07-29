using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class QualifiedIdentifierPart
    {
        public IdentifierToken Identifier { get; private set; }
        public DotToken? Dot { get; private set; }

        public QualifiedIdentifierPart(TokenCollection tokens, IdentifierToken? identifier = null)
        {
            Identifier = identifier ?? tokens.PopToken<IdentifierToken>();
            if (tokens.PopIfMatches(out DotToken dot))
            {
                Dot = dot;
            }
        }

        public override string ToString()
        {
            string ret = "";
            ret += Identifier.ToString();
            ret += Dot?.ToString();
            return ret;
        }
    }
}
