using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class PreDecrementExpression : UnaryExpression
    {
        public readonly DecrementToken PreDecrement;
        public readonly UnaryExpression Expression;

        public override IToken LeftToken => PreDecrement;
        public override IToken RightToken => Expression.RightToken;

        public PreDecrementExpression(TokenCollection tokens, DecrementToken? preDecrement = null, UnaryExpression expression = null)
        {
            PreDecrement = preDecrement == null ? tokens.PopToken<DecrementToken>() : (DecrementToken)preDecrement;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
