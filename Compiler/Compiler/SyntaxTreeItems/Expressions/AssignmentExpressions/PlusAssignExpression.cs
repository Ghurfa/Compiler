using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class PlusAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly PlusAssignToken PlusAssign;
        public readonly Expression From;

        public override IToken LeftToken => To.LeftToken;
        public override IToken RightToken => From.RightToken;

        public PlusAssignExpression(TokenCollection tokens, UnaryExpression to = null, PlusAssignToken? plusAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            PlusAssign = plusAssign == null ? tokens.PopToken<PlusAssignToken>() : (PlusAssignToken)plusAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
