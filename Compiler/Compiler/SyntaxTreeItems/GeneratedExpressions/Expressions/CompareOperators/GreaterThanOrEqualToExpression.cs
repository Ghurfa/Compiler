using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class GreaterThanOrEqualToExpression : Expression
    {
        public override int Precedence => 5;

        public Expression Left { get; private set; }
        public GreaterThanOrEqualToToken GreaterThanOrEqualTo { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public GreaterThanOrEqualToExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            GreaterThanOrEqualTo = tokens.PopToken<GreaterThanOrEqualToToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += GreaterThanOrEqualTo.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
