using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class EqualsExpression : Expression
    {
        public Expression Left { get; private set; }
        public EqualsToken Equals { get; private set; }
        public Expression Right { get; private set; }

        public override int Precedence => 6;

        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public EqualsExpression(TokenCollection tokens, Expression left = null, EqualsToken? equals = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Equals = equals == null ? tokens.PopToken<EqualsToken>() : (EqualsToken)equals;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += Equals.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
