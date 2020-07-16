using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PlusExpression : Expression
    {
        public override int Precedence => 3;

        public Expression Left { get; private set; }
        public PlusToken Plus { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public PlusExpression(TokenCollection tokens, Expression left = null, PlusToken? plus = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Plus = plus == null ? tokens.PopToken<PlusToken>() : (PlusToken)plus;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += Plus.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
