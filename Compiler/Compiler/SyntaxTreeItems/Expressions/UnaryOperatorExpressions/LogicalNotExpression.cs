using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class LogicalNotExpression : UnaryExpression
    {
        public readonly NotToken LogicalNot;
        public readonly UnaryExpression Expression;

        public override IToken LeftToken => LogicalNot;
        public override IToken RightToken => Expression.RightToken;

        public LogicalNotExpression(TokenCollection tokens, NotToken? logicalNot = null, UnaryExpression expression = null)
        {
            LogicalNot = logicalNot == null ? tokens.PopToken<NotToken>() : (NotToken)logicalNot;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
