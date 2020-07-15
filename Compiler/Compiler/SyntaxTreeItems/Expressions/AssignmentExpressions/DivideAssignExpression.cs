using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class DivideAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly DivideAssignToken DivideAssign;
        public readonly Expression From;

        public override IToken LeftToken => To.LeftToken;
        public override IToken RightToken => From.RightToken;

        public DivideAssignExpression(TokenCollection tokens, UnaryExpression to = null, DivideAssignToken? divideAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            DivideAssign = divideAssign == null ? tokens.PopToken<DivideAssignToken>() : (DivideAssignToken)divideAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
