using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class MultiplyAssignExpression : Expression, ICompleteStatement
    {
        public UnaryExpression To { get; private set; }
        public MultiplyAssignToken MultiplyAssign { get; private set; }
        public Expression From { get; private set; }

        public override int Precedence => 14;

        public override Expression LeftExpr { get => To; set => throw new InvalidOperationException() }
        public override Expression RightExpr { get => To; set => throw new InvalidOperationException() }

        public MultiplyAssignExpression(TokenCollection tokens, UnaryExpression to = null, MultiplyAssignToken? multiplyAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            MultiplyAssign = multiplyAssign == null ? tokens.PopToken<MultiplyAssignToken>() : (MultiplyAssignToken)multiplyAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += MultiplyAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
