using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class BitwiseOrExpression : Expression
    {
        public readonly Expression Left;
        public readonly BitwiseOrToken BitwiseOr;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public BitwiseOrExpression(TokenCollection tokens, Expression left = null, BitwiseOrToken? bitwiseOr = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            BitwiseOr = bitwiseOr == null ? tokens.PopToken<BitwiseOrToken>() : (BitwiseOrToken)bitwiseOr;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
