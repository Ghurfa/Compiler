using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class CastExpression : Expression
    {
        public readonly UnaryExpression Expression;
        public readonly IToken AsToken;
        public readonly Type CastTo;

        public CastExpression(TokenCollection tokens, UnaryExpression expression)
        {
            Expression = expression;
            AsToken = tokens.PopToken(TokenType.AsKeyword);
            CastTo = Type.ReadType(tokens);
        }
        public override string ToString()
        {
            return Expression.ToString() + " " + AsToken.Text + " " + CastTo.ToString();
        }
    }
}
