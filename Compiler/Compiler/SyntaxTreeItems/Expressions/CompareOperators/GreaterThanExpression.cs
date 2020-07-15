using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class GreaterThanExpression : Expression
    {
        public readonly Expression Left;
        public readonly GreaterThanToken GreaterThan;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public GreaterThanExpression(TokenCollection tokens, Expression left = null, GreaterThanToken? greaterThan = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            GreaterThan = greaterThan == null ? tokens.PopToken<GreaterThanToken>() : (GreaterThanToken)greaterThan;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
