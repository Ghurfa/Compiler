using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class EqualsExpression : Expression
    {
        public readonly Expression Left;
        public readonly EqualsToken Equals;
        public readonly Expression Right;

        public EqualsExpression(TokenCollection tokens, Expression left = null, EqualsToken? equals = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Equals = equals == null ? tokens.PopToken<EqualsToken>() : (EqualsToken)equals;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
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
