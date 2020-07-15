using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class GreaterThanOrEqualToExpression : Expression
    {
        public readonly Expression Left;
        public readonly GreaterThanOrEqualToToken GreaterThanOrEqualTo;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public GreaterThanOrEqualToExpression(TokenCollection tokens, Expression left = null, GreaterThanOrEqualToToken? greaterThanOrEqualTo = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            GreaterThanOrEqualTo = greaterThanOrEqualTo == null ? tokens.PopToken<GreaterThanOrEqualToToken>() : (GreaterThanOrEqualToToken)greaterThanOrEqualTo;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
