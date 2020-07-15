using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class NullCoalescingAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly NullCoalescingAssignToken NullCoalescingAssign;
        public readonly Expression From;

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
