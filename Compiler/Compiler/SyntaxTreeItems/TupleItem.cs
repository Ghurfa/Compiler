using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class TupleItem
    {
        public readonly Expression Expression;
        public readonly Token? CommaToken;

        public TupleItem(TokenCollection tokens)
        {
            Expression = Expression.ReadExpression(tokens);
            if (tokens.PopIfMatches(out Token comma, TokenType.Comma))
            {
                CommaToken = comma;
            }
        }

        public TupleItem(Expression expression, Token commaToken)
        {
            Expression = expression;
            CommaToken = commaToken;
        }
        public override string ToString()
        {
            return Expression.ToString() + CommaToken?.ToString();
        }
    }
}
