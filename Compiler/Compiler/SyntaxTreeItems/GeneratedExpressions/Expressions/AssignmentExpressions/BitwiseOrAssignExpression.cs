using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseOrAssignExpression : Expression, ICompleteStatement
    {
        public UnaryExpression To { get; private set; }
        public BitwiseOrAssignToken BitwiseOrAssign { get; private set; }
        public Expression From { get; private set; }

        public override int Precedence => 14;

        public override Expression LeftExpr { get => To; set => throw new InvalidOperationException() }
        public override Expression RightExpr { get => To; set => throw new InvalidOperationException() }

        public BitwiseOrAssignExpression(TokenCollection tokens, UnaryExpression to = null, BitwiseOrAssignToken? bitwiseOrAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            BitwiseOrAssign = bitwiseOrAssign == null ? tokens.PopToken<BitwiseOrAssignToken>() : (BitwiseOrAssignToken)bitwiseOrAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += BitwiseOrAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
