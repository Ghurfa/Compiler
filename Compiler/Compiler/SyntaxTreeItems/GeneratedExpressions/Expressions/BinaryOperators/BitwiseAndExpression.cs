using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseAndExpression : Expression
    {
        public Expression Left { get; private set; }
        public BitwiseAndToken BitwiseAnd { get; private set; }
        public Expression Right { get; private set; }

        public override int Precedence => 7;

        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public BitwiseAndExpression(TokenCollection tokens, Expression left = null, BitwiseAndToken? bitwiseAnd = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            BitwiseAnd = bitwiseAnd == null ? tokens.PopToken<BitwiseAndToken>() : (BitwiseAndToken)bitwiseAnd;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += BitwiseAnd.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
