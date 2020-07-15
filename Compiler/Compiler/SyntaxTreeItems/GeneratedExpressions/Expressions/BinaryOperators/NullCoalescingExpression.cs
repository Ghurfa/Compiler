using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class NullCoalescingExpression : Expression
    {
        public readonly Expression Left;
        public readonly NullCoalescingToken NullCoalescing;
        public readonly Expression Right;

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
            ret += NullCoalescing.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
