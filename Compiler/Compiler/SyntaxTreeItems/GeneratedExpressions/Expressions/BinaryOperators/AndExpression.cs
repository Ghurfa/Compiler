using System;
using System.Collections.Generic;
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

        public AndExpression(TokenCollection tokens, Expression left = null, AndToken? and = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            And = and == null ? tokens.PopToken<AndToken>() : (AndToken)and;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += And.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
