using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class BitwiseNotExpression : UnaryExpression
    {
        public readonly BitwiseNotToken BitwiseNot;
        public readonly UnaryExpression Expression;

        public override IToken LeftToken => BitwiseNot;
        public override IToken RightToken => Expression.RightToken;

        public BitwiseNotExpression(TokenCollection tokens, BitwiseNotToken? bitwiseNot = null, UnaryExpression expression = null)
        {
            BitwiseNot = bitwiseNot == null ? tokens.PopToken<BitwiseNotToken>() : (BitwiseNotToken)bitwiseNot;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
