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
        public override Expression RightExpr { get => Left; set { Left = value; } }

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
            ret += Divide.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
