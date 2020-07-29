using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class Argument
    {
        public Expression Expression { get; private set; }
        public CommaToken? Comma { get; private set; }

        public Argument(TokenCollection tokens)
        {
            Expression = Expression.ReadExpression(tokens);
            if (tokens.PopIfMatches(out CommaToken comma))
            {
                Comma = comma;
            }
        }

        public override string ToString()
        {
            string ret = "";
            ret += Expression.ToString();
            ret += Comma?.ToString();
            return ret;
        }
    }
}
