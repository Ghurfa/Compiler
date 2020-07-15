using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class NullCoalescingExpression : Expression
    {
        public readonly Expression Left;
        public readonly NullCoalescingToken NullCoalescing;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public NullCoalescingExpression(TokenCollection tokens, Expression left = null, NullCoalescingToken? nullCoalescing = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            NullCoalescing = nullCoalescing == null ? tokens.PopToken<NullCoalescingToken>() : (NullCoalescingToken)nullCoalescing;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
