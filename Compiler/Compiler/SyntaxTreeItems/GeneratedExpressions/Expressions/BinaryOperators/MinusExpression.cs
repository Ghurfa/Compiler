using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class MinusExpression : Expression
    {
        public readonly Expression Left;
        public readonly MinusToken Minus;
        public readonly Expression Right;

        public MinusExpression(TokenCollection tokens, Expression left = null, MinusToken? minus = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Minus = minus == null ? tokens.PopToken<MinusToken>() : (MinusToken)minus;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += Minus.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
