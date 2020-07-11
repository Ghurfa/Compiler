using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class CastExpression : Expression
    {
        public readonly UnaryExpression Expression;
        public readonly Token AsToken;
        public readonly TypeToken CastTo;

        public CastExpression(TokenCollection tokens, UnaryExpression expression, Token asToken)
        {
            Expression = expression;
            AsToken = asToken;
            CastTo = new TypeToken(tokens);
        }
    }
}
