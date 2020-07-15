using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseXorExpression : Expression
    {
        public readonly Expression Left;
        public readonly BitwiseXorToken BitwiseXor;
        public readonly Expression Right;

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
            ret += BitwiseXor.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
