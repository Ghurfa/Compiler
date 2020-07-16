using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class PlusAssignExpression : Expression, ICompleteStatement
    {
        public UnaryExpression To { get; private set; }
        public PlusAssignToken PlusAssign { get; private set; }
        public Expression From { get; private set; }

        public override int Precedence => 14;

        public override Expression LeftExpr { get => To; set { if (value is UnaryExpression unary) To = unary; else throw new InvalidAssignmentLeftException(value);} }
        public override Expression RightExpr { get => From; set { From = value; } }

        public PlusAssignExpression(TokenCollection tokens, UnaryExpression to = null, PlusAssignToken? plusAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            PlusAssign = plusAssign == null ? tokens.PopToken<PlusAssignToken>() : (PlusAssignToken)plusAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += " ";
            ret += PlusAssign.ToString();
            ret += " ";
            ret += From.ToString();
            return ret;
        }
    }
}
