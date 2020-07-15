using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class DeclAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly DeclAssignToken DeclAssign;
        public readonly Expression From;

        public override IToken LeftToken => To.LeftToken;
        public override IToken RightToken => From.RightToken;

        public DeclAssignExpression(TokenCollection tokens, UnaryExpression to = null, DeclAssignToken? declAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            DeclAssign = declAssign == null ? tokens.PopToken<DeclAssignToken>() : (DeclAssignToken)declAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
