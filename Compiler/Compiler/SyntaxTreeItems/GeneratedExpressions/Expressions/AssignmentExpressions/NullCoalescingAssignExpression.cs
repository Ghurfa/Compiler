using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class NullCoalescingAssignExpression : Expression, ICompleteStatement
    {
        public UnaryExpression To { get; private set; }
        public NullCoalescingAssignToken NullCoalescingAssign { get; private set; }
        public Expression From { get; private set; }

        public override int Precedence => 14;

        public override Expression LeftExpr { get => To; set => throw new InvalidOperationException() }
        public override Expression RightExpr { get => To; set => throw new InvalidOperationException() }

        public NullCoalescingAssignExpression(TokenCollection tokens, UnaryExpression to = null, NullCoalescingAssignToken? nullCoalescingAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            NullCoalescingAssign = nullCoalescingAssign == null ? tokens.PopToken<NullCoalescingAssignToken>() : (NullCoalescingAssignToken)nullCoalescingAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += NullCoalescingAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
