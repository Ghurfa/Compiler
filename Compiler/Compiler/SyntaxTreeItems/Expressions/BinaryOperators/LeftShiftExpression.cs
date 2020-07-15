using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class LeftShiftExpression : Expression
    {
        public readonly Expression Left;
        public readonly LeftShiftToken LeftShift;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public LeftShiftExpression(TokenCollection tokens, Expression left = null, LeftShiftToken? leftShift = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            LeftShift = leftShift == null ? tokens.PopToken<LeftShiftToken>() : (LeftShiftToken)leftShift;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
