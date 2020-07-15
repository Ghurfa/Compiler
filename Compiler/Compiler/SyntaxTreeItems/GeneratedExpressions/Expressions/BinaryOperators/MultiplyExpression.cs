using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class MultiplyExpression : Expression
    {
        public readonly Expression Left;
        public readonly AsteriskToken Multiply;
        public readonly Expression Right;

        public MultiplyExpression(TokenCollection tokens, Expression left = null, AsteriskToken? multiply = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Multiply = multiply == null ? tokens.PopToken<AsteriskToken>() : (AsteriskToken)multiply;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += Multiply.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
