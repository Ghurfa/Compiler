using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MultiplyExpression : Expression
    {
        public override int Precedence => 2;

        public Expression Left { get; private set; }
        public AsteriskToken Multiply { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public MultiplyExpression(TokenCollection tokens, Expression left = null, AsteriskToken? multiply = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Multiply = multiply == null ? tokens.PopToken<AsteriskToken>() : (AsteriskToken)multiply;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += Multiply.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
