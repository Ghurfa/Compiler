using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.UnaryExpressions
{
    public class BitwiseNotExpression : UnaryExpression
    {
        public readonly Token BitwiseNotOperator;
        public readonly UnaryExpression Expression;
        public BitwiseNotExpression(LinkedList<Token> tokens, Token notOperator)
        {
            BitwiseNotOperator = notOperator;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }
    }
}
