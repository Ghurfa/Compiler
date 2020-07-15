using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class NotEqualsExpression : Expression
    {
        public readonly Expression Left;
        public readonly NotEqualsToken NotEquals;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public NotEqualsExpression(TokenCollection tokens, Expression left = null, NotEqualsToken? notEquals = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            NotEquals = notEquals == null ? tokens.PopToken<NotEqualsToken>() : (NotEqualsToken)notEquals;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
