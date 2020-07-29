using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class PlusExpression : Expression
    {
        public override int Precedence => 3;

        public Expression Left { get; private set; }
        public PlusToken Plus { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public PlusExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            Plus = tokens.PopToken<PlusToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += Plus.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
