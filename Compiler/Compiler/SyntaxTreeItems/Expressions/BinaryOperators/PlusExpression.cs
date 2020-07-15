using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class PlusExpression : Expression
    {
        public readonly Expression Left;
        public readonly PlusToken Plus;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public PlusExpression(TokenCollection tokens, Expression left = null, PlusToken? plus = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Plus = plus == null ? tokens.PopToken<PlusToken>() : (PlusToken)plus;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
