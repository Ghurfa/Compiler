using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class EqualsExpression : Expression
    {
        public readonly Expression Left;
        public readonly EqualsToken Equals;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public EqualsExpression(TokenCollection tokens, Expression left = null, EqualsToken? equals = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Equals = equals == null ? tokens.PopToken<EqualsToken>() : (EqualsToken)equals;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
