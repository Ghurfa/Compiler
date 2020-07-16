using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class RightShiftExpression : Expression
    {
        public override int Precedence => 4;

        public Expression Left { get; private set; }
        public RightShiftToken RightShift { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public RightShiftExpression(TokenCollection tokens, Expression left = null, RightShiftToken? rightShift = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            RightShift = rightShift == null ? tokens.PopToken<RightShiftToken>() : (RightShiftToken)rightShift;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += RightShift.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
