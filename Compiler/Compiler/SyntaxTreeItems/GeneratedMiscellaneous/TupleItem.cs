using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class TupleItem
    {
        public Expression Expression { get; private set; }
        public CommaToken? Comma { get; private set; }

        public TupleItem(TokenCollection tokens, Expression expression = null)
        {
            Expression = expression ?? Expression.ReadExpression(tokens);
            if (tokens.PopIfMatches(out CommaToken comma))
            {
                Comma = comma;
            }
        }

        public override string ToString()
        {
            string ret = "";
            ret += Expression.ToString();
            ret += " ";
            ret += Comma.ToString();
            return ret;
        }
    }
}
