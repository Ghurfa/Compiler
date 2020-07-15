using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class TupleItem
    {
        public readonly Expression Expression;
        public readonly CommaToken? Comma;

        public TupleItem(TokenCollection tokens)
        {
            Expression = Expression.ReadExpression(tokens);
            if (tokens.PopIfMatches(out CommaToken comma))
            {
                Comma = comma;
            }
        }

        public TupleItem(Expression expression, CommaToken commaToken)
        {
            Expression = expression;
            Comma = commaToken;
        }
        public override string ToString()
        {
            return Expression.ToString() + Comma?.ToString();
        }
    }
}
