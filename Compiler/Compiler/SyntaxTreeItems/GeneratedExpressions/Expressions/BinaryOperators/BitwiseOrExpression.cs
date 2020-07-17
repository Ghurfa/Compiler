using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseOrExpression : Expression
    {
        public override int Precedence => 9;

        public Expression Left { get; private set; }
        public BitwiseOrToken BitwiseOr { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public BitwiseOrExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            BitwiseOr = tokens.PopToken<BitwiseOrToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += BitwiseOr.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
