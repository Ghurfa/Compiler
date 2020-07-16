using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class GreaterThanOrEqualToExpression : Expression
    {
        public Expression Left { get; private set; }
        public GreaterThanOrEqualToToken GreaterThanOrEqualTo { get; private set; }
        public Expression Right { get; private set; }

        public override int Precedence => 5;

        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Left; set { Left = value; } }

        public GreaterThanOrEqualToExpression(TokenCollection tokens, Expression left = null, GreaterThanOrEqualToToken? greaterThanOrEqualTo = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            GreaterThanOrEqualTo = greaterThanOrEqualTo == null ? tokens.PopToken<GreaterThanOrEqualToToken>() : (GreaterThanOrEqualToToken)greaterThanOrEqualTo;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += GreaterThanOrEqualTo.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
