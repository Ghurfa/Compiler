using System;
using System.Collections.Generic;
using System.Linq;
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

        public RightShiftExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            RightShift = tokens.PopToken<RightShiftToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += RightShift.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
