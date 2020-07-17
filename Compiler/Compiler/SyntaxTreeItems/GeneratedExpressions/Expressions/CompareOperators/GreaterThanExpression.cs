using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class GreaterThanExpression : Expression
    {
        public override int Precedence => 5;

        public Expression Left { get; private set; }
        public GreaterThanToken GreaterThan { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public GreaterThanExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            GreaterThan = tokens.PopToken<GreaterThanToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += GreaterThan.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
