using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MinusExpression : Expression
    {
        public override int Precedence => 3;

        public Expression Left { get; private set; }
        public MinusToken Minus { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public MinusExpression(TokenCollection tokens, Expression left = null, MinusToken? minus = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Minus = minus == null ? tokens.PopToken<MinusToken>() : (MinusToken)minus;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += Minus.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
