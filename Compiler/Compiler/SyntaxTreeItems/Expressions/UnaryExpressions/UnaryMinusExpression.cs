using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.UnaryExpressions
{
    public class UnaryMinusExpression : UnaryExpression
    {
        public readonly Token MinusOperator;
        public readonly UnaryExpression Expression;
        public UnaryMinusExpression(LinkedList<Token> tokens, Token minusOperator)
        {
            MinusOperator = minusOperator;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }
    }
}
