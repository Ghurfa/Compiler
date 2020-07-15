using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class BitwiseAndExpression : Expression
    {
        public readonly Expression Left;
        public readonly BitwiseAndToken BitwiseAnd;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public BitwiseAndExpression(TokenCollection tokens, Expression left = null, BitwiseAndToken? bitwiseAnd = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            BitwiseAnd = bitwiseAnd == null ? tokens.PopToken<BitwiseAndToken>() : (BitwiseAndToken)bitwiseAnd;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
