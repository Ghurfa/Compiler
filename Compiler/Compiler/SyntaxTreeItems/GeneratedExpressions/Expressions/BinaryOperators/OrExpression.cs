using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class OrExpression : Expression
    {
        public override int Precedence => 11;

        public Expression Left { get; private set; }
        public OrToken Or { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public OrExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            Or = tokens.PopToken<OrToken>();;
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += Or.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
