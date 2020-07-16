using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class OrExpression : Expression
    {
        public Expression Left { get; private set; }
        public OrToken Or { get; private set; }
        public Expression Right { get; private set; }

        public override int Precedence => 11;

        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Left; set { Left = value; } }

        public OrExpression(TokenCollection tokens, Expression left = null, OrToken? or = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Or = or == null ? tokens.PopToken<OrToken>() : (OrToken)or;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += Or.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
