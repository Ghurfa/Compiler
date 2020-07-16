using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class MinusAssignExpression : Expression, ICompleteStatement
    {
        public UnaryExpression To { get; private set; }
        public MinusAssignToken MinusAssign { get; private set; }
        public Expression From { get; private set; }

        public override int Precedence => 14;

        public override Expression LeftExpr { get => To; set => throw new InvalidOperationException() }
        public override Expression RightExpr { get => To; set => throw new InvalidOperationException() }

        public MinusAssignExpression(TokenCollection tokens, UnaryExpression to = null, MinusAssignToken? minusAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            MinusAssign = minusAssign == null ? tokens.PopToken<MinusAssignToken>() : (MinusAssignToken)minusAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += MinusAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
