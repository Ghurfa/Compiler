using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class DeclAssignExpression : Expression, ICompleteStatement
    {
        public override int Precedence => 14;

        private UnaryExpression to;
        public UnaryExpression To { get => to; set { if (value is IAssignableExpression) to = value; else throw new InvalidAssignmentLeftException(value); } }
        public DeclAssignToken DeclAssign { get; private set; }
        public Expression From { get; private set; }
        public override Expression LeftExpr { get => To; set { if (value is UnaryExpression unary) To = unary; else throw new InvalidAssignmentLeftException(value);} }
        public override Expression RightExpr { get => From; set { From = value; } }

        public DeclAssignExpression(TokenCollection tokens, UnaryExpression to = null, DeclAssignToken? declAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            DeclAssign = declAssign == null ? tokens.PopToken<DeclAssignToken>() : (DeclAssignToken)declAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += " ";
            ret += DeclAssign.ToString();
            ret += " ";
            ret += From.ToString();
            return ret;
        }
    }
}
