using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class LogicalNotExpression : UnaryExpression
    {
        public NotToken LogicalNot { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public LogicalNotExpression(TokenCollection tokens)
        {
            LogicalNot = tokens.PopToken<NotToken>();;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
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
