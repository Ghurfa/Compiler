using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class LeftShiftAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly LeftShiftAssignToken LeftShiftAssign;
        public readonly Expression From;

        public override IToken LeftToken => To.LeftToken;
        public override IToken RightToken => From.RightToken;

        public LeftShiftAssignExpression(TokenCollection tokens, UnaryExpression to = null, LeftShiftAssignToken? leftShiftAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            LeftShiftAssign = leftShiftAssign == null ? tokens.PopToken<LeftShiftAssignToken>() : (LeftShiftAssignToken)leftShiftAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
