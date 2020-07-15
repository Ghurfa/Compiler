using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class DereferenceExpression : UnaryExpression
    {
        public readonly AsteriskToken Dereference;
        public readonly UnaryExpression Expression;

        public override IToken LeftToken => Dereference;
        public override IToken RightToken => Expression.RightToken;

        public DereferenceExpression(TokenCollection tokens, AsteriskToken? dereference = null, UnaryExpression expression = null)
        {
            Dereference = dereference == null ? tokens.PopToken<AsteriskToken>() : (AsteriskToken)dereference;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
