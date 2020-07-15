using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class Argument
    {
        public readonly Expression Expression;
        public readonly CommaToken? Comma;

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
            return Expression.ToString() + Comma?.ToString();
        }
    }
}
