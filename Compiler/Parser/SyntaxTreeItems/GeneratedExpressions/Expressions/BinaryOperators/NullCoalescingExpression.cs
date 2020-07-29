using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class NullCoalescingExpression : Expression
    {
        public override int Precedence => 12;

        public Expression Left { get; private set; }
        public NullCoalescingToken NullCoalescing { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public NullCoalescingExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            NullCoalescing = tokens.PopToken<NullCoalescingToken>();
            Right = Expression.ReadExpression(tokens);
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
