using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.UnaryExpressions
{
    public class LogicalNotExpression : UnaryExpression
    {
        public readonly Token LogicalNotOperator;
        public readonly UnaryExpression Expression;
        public LogicalNotExpression(TokenCollection tokens, Token notOperator)
        {
            LogicalNotOperator = notOperator;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }
    }
}
