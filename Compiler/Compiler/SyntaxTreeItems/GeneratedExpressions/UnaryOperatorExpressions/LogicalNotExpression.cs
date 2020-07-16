using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class LogicalNotExpression : UnaryExpression
    {
        public NotToken LogicalNot { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public LogicalNotExpression(TokenCollection tokens, NotToken? logicalNot = null, UnaryExpression expression = null)
        {
            LogicalNot = logicalNot == null ? tokens.PopToken<NotToken>() : (NotToken)logicalNot;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += LogicalNot.ToString();
            ret += " ";
            ret += Expression.ToString();
            return ret;
        }
    }
}
