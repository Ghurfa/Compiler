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

        public override Expression LeftExpr { get => To; set { if (value is UnaryExpression unary) To = unary; else throw new InvalidAssignmentLeftException(value);} }
        public override Expression RightExpr { get => From; set { From = value; } }

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
            ret += " ";
            ret += Assign.ToString();
            ret += " ";
            ret += From.ToString();
            return ret;
        }
    }
}
