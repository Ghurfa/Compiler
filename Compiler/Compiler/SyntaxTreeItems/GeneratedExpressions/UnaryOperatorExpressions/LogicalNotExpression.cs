using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class LogicalNotExpression : UnaryExpression
    {
        public readonly NotToken LogicalNot;
        public readonly UnaryExpression Expression;

        public LogicalNotExpression(TokenCollection tokens, NotToken? logicalNot = null, UnaryExpression expression = null)
        {
            LogicalNot = logicalNot == null ? tokens.PopToken<NotToken>() : (NotToken)logicalNot;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += LogicalNot.ToString();
            ret += Expression.ToString();
            return ret;
        }
    }
}
