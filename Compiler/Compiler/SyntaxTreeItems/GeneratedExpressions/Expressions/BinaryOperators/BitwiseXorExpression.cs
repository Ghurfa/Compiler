using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseXorExpression : Expression
    {
        public override int Precedence => 8;

        public Expression Left { get; private set; }
        public BitwiseXorToken BitwiseXor { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public BitwiseXorExpression(TokenCollection tokens, Expression left = null, BitwiseXorToken? bitwiseXor = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            BitwiseXor = bitwiseXor == null ? tokens.PopToken<BitwiseXorToken>() : (BitwiseXorToken)bitwiseXor;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += " ";
            ret += BitwiseXor.ToString();
            ret += " ";
            ret += Right.ToString();
            return ret;
        }
    }
}
