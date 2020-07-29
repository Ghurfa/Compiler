using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class NotEqualsExpression : Expression
    {
        public override int Precedence => 6;

        public Expression Left { get; private set; }
        public NotEqualsToken NotEquals { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public NotEqualsExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            NotEquals = tokens.PopToken<NotEqualsToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += NotEquals.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
