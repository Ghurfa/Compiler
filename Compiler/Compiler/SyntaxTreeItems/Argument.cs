using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class Argument
    {
        public readonly Expression Expression;
        public readonly Token? CommaToken;

        public Argument(TokenCollection tokens)
        {
            Expression = Expression.ReadExpression(tokens);
            if (tokens.PopIfMatches(out Token comma, TokenType.SyntaxChar, ","))
            {
                CommaToken = comma;
            }
        }
    }
}
