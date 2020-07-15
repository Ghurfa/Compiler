using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class PlusExpression : Expression
    {
        public readonly Expression Left;
        public readonly PlusToken Plus;
        public readonly Expression Right;

        public PlusExpression(TokenCollection tokens, Expression left = null, PlusToken? plus = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Plus = plus == null ? tokens.PopToken<PlusToken>() : (PlusToken)plus;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
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
