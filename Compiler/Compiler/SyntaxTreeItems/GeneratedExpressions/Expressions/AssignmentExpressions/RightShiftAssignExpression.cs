using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class RightShiftAssignExpression : Expression, ICompleteStatement
    {
        public UnaryExpression To { get; private set; }
        public RightShiftAssignToken RightShiftAssign { get; private set; }
        public Expression From { get; private set; }

        public override int Precedence => 14;

        public override Expression LeftExpr { get => To; set => throw new InvalidOperationException() }
        public override Expression RightExpr { get => To; set => throw new InvalidOperationException() }

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
