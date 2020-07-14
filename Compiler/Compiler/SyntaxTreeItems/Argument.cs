using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class Argument
    {
        public readonly Expression Expression;
        public readonly IToken? CommaToken;

        public Argument(TokenCollection tokens)
        {
            Expression = Expression.ReadExpression(tokens);
            if (tokens.PopIfMatches(out IToken comma, TokenType.Comma))
            {
                CommaToken = comma;
            }
        }
        public override string ToString()
        {
            return Expression.ToString() + CommaToken?.ToString();
        }
    }
}
