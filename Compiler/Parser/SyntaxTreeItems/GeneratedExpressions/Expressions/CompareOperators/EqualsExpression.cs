using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class EqualsExpression : Expression
    {
        public override int Precedence => 6;

        public Expression Left { get; private set; }
        public EqualsToken Equals { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public EqualsExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            Equals = tokens.PopToken<EqualsToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += Equals.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
