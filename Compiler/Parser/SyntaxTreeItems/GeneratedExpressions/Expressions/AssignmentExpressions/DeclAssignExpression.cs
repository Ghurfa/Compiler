using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
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

        public DeclAssignExpression(TokenCollection tokens, UnaryExpression to)
        {
            To = to;
            DeclAssign = tokens.PopToken<DeclAssignToken>();
            From = Expression.ReadExpression(tokens);
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
