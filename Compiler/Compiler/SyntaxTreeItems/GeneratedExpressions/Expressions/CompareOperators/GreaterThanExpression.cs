using System;
using System.Collections.Generic;
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

        public GreaterThanExpression(TokenCollection tokens, Expression left = null, GreaterThanToken? greaterThan = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            GreaterThan = greaterThan == null ? tokens.PopToken<GreaterThanToken>() : (GreaterThanToken)greaterThan;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
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
