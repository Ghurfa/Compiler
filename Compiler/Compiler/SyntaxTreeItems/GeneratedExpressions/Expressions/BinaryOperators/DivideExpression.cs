using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class DivideExpression : Expression
    {
        public Expression Left { get; private set; }
        public DivideToken Divide { get; private set; }
        public Expression Right { get; private set; }

        public override int Precedence => 2;

        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public DivideExpression(TokenCollection tokens, Expression left = null, DivideToken? divide = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Divide = divide == null ? tokens.PopToken<DivideToken>() : (DivideToken)divide;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += Divide.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
