using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class BitwiseXorExpression : Expression
    {
        public readonly Expression Left;
        public readonly BitwiseXorToken BitwiseXor;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public BitwiseXorExpression(TokenCollection tokens, Expression left = null, BitwiseXorToken? bitwiseXor = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            BitwiseXor = bitwiseXor == null ? tokens.PopToken<BitwiseXorToken>() : (BitwiseXorToken)bitwiseXor;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
