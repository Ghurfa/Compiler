using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class DeclAssignExpression : Expression, ICompleteStatement
    {
        public UnaryExpression To { get; private set; }
        public DeclAssignToken DeclAssign { get; private set; }
        public Expression From { get; private set; }

        public override int Precedence => 14;

        public override Expression LeftExpr { get => To; set => throw new InvalidOperationException() }
        public override Expression RightExpr { get => To; set => throw new InvalidOperationException() }

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
            ret += DeclAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
