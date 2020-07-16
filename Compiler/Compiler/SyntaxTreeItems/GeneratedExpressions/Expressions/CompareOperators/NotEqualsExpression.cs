using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class NotEqualsExpression : Expression
    {
        public Expression Left { get; private set; }
        public NotEqualsToken NotEquals { get; private set; }
        public Expression Right { get; private set; }

        public override int Precedence => 6;

        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public NotEqualsExpression(TokenCollection tokens, Expression left = null, NotEqualsToken? notEquals = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            NotEquals = notEquals == null ? tokens.PopToken<NotEqualsToken>() : (NotEqualsToken)notEquals;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += NotEquals.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
