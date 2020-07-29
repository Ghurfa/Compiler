using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class BitwiseAndExpression : Expression
    {
        public override int Precedence => 7;

        public Expression Left { get; private set; }
        public BitwiseAndToken BitwiseAnd { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public BitwiseAndExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            BitwiseAnd = tokens.PopToken<BitwiseAndToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += BitwiseAnd.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
