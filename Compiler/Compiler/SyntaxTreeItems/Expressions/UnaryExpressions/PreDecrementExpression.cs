using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.UnaryExpressions
{
    public class PreDecrementExpression : UnaryExpression
    {
        public readonly Token DecrementOperator;
        public readonly UnaryExpression Expression;
        public PreDecrementExpression(TokenCollection tokens, Token decrOperator)
        {
            DecrementOperator = decrOperator;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }
    }
}
