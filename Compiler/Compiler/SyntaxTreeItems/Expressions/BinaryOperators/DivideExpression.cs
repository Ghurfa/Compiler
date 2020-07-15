using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class DivideExpression : Expression
    {
        public readonly Expression Left;
        public readonly DivideToken Divide;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public DivideExpression(TokenCollection tokens, Expression left = null, DivideToken? divide = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Divide = divide == null ? tokens.PopToken<DivideToken>() : (DivideToken)divide;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
