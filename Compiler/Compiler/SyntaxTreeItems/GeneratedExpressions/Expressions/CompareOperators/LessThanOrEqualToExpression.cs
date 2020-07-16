using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class LessThanOrEqualToExpression : Expression
    {
        public override int Precedence => 5;

        public Expression Left { get; private set; }
        public LessThanOrEqualToToken LessThanOrEqualTo { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public LessThanOrEqualToExpression(TokenCollection tokens, Expression left = null, LessThanOrEqualToToken? lessThanOrEqualTo = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            LessThanOrEqualTo = lessThanOrEqualTo == null ? tokens.PopToken<LessThanOrEqualToToken>() : (LessThanOrEqualToToken)lessThanOrEqualTo;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += LessThanOrEqualTo.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
