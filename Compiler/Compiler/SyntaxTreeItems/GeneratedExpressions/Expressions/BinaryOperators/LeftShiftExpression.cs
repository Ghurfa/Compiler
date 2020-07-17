using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class LeftShiftExpression : Expression
    {
        public override int Precedence => 4;

        public Expression Left { get; private set; }
        public LeftShiftToken LeftShift { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public LeftShiftExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            LeftShift = tokens.PopToken<LeftShiftToken>();;
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += LeftShift.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
