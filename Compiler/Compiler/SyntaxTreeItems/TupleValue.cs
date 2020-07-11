using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class TupleValue
    {
        public readonly Expression Expression;
        public readonly Token? CommaToken;

        public TupleValue(TokenCollection tokens)
        {
            Expression = Expression.ReadExpression(tokens);
            if (tokens.PopIfMatches(out Token comma, TokenType.Comma))
            {
                CommaToken = comma;
            }
        }

        public TupleValue(Expression expression, Token commaToken)
        {
            Expression = expression;
            CommaToken = commaToken;
        }
    }
}
