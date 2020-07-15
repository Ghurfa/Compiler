using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class RightShiftExpression : Expression
    {
        public readonly Expression Left;
        public readonly RightShiftToken RightShift;
        public readonly Expression Right;

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
            ret += RightShift.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
