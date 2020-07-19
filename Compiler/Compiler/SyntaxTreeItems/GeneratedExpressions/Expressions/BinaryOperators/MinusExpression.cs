using System;
using System.Collections.Generic;
using System.Linq;
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

        public MinusExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            Minus = tokens.PopToken<MinusToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += Minus.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
