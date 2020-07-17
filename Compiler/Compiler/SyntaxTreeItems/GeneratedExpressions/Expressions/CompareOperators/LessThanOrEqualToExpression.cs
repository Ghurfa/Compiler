using System;
using System.Collections.Generic;
using System.Linq;
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

        public LessThanOrEqualToExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            LessThanOrEqualTo = tokens.PopToken<LessThanOrEqualToToken>();;
            Right = Expression.ReadExpression(tokens);
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
