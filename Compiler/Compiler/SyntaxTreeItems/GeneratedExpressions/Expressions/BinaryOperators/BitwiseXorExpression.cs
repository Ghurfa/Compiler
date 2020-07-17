using System;
using System.Collections.Generic;
using System.Linq;
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

        public BitwiseXorExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            BitwiseXor = tokens.PopToken<BitwiseXorToken>();;
            Right = Expression.ReadExpression(tokens);
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
