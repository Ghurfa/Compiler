using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class AssignExpression : Expression, ICompleteStatement
    {
        public UnaryExpression To { get; private set; }
        public AssignToken Assign { get; private set; }
        public Expression From { get; private set; }

        public override int Precedence => 14;

        public override Expression LeftExpr { get => To; set => throw new InvalidOperationException() }
        public override Expression RightExpr { get => To; set => throw new InvalidOperationException() }

        public AssignExpression(TokenCollection tokens, UnaryExpression to = null, AssignToken? assign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            Assign = assign == null ? tokens.PopToken<AssignToken>() : (AssignToken)assign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += Assign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
