using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class NullCoalescingExpression : Expression
    {
        public override int Precedence => 12;

        public Expression Left { get; private set; }
        public NullCoalescingToken NullCoalescing { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public NullCoalescingExpression(TokenCollection tokens, Expression left = null, NullCoalescingToken? nullCoalescing = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            NullCoalescing = nullCoalescing == null ? tokens.PopToken<NullCoalescingToken>() : (NullCoalescingToken)nullCoalescing;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += NullCoalescing.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
