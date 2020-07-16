using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class LeftShiftExpression : Expression
    {
        public Expression Left { get; private set; }
        public LeftShiftToken LeftShift { get; private set; }
        public Expression Right { get; private set; }

        public override int Precedence => 4;

        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Left; set { Left = value; } }

        public LeftShiftExpression(TokenCollection tokens, Expression left = null, LeftShiftToken? leftShift = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            LeftShift = leftShift == null ? tokens.PopToken<LeftShiftToken>() : (LeftShiftToken)leftShift;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += LeftShift.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
