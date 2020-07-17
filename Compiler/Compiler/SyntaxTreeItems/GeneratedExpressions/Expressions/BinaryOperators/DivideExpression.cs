using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class DivideExpression : Expression
    {
        public override int Precedence => 2;

        public Expression Left { get; private set; }
        public DivideToken Divide { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public DivideExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            Divide = tokens.PopToken<DivideToken>();
            Right = Expression.ReadExpression(tokens);
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
