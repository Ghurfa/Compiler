using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseOrExpression : Expression
    {
        public readonly Expression Left;
        public readonly BitwiseOrToken BitwiseOr;
        public readonly Expression Right;

        public BitwiseOrExpression(TokenCollection tokens, Expression left = null, BitwiseOrToken? bitwiseOr = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            BitwiseOr = bitwiseOr == null ? tokens.PopToken<BitwiseOrToken>() : (BitwiseOrToken)bitwiseOr;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += BitwiseOr.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
