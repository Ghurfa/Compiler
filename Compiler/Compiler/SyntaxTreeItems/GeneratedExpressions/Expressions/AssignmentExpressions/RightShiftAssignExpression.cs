using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class RightShiftAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly RightShiftAssignToken RightShiftAssign;
        public readonly Expression From;

        public RightShiftAssignExpression(TokenCollection tokens, UnaryExpression to = null, RightShiftAssignToken? rightShiftAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            RightShiftAssign = rightShiftAssign == null ? tokens.PopToken<RightShiftAssignToken>() : (RightShiftAssignToken)rightShiftAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += RightShiftAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
