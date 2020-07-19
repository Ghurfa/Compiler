using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class AndExpression : Expression
    {
        public override int Precedence => 10;

        public Expression Left { get; private set; }
        public AndToken And { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public AndExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            And = tokens.PopToken<AndToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += And.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
