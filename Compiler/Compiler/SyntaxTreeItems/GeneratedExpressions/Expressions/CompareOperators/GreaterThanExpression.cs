using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class GreaterThanExpression : Expression
    {
        public readonly Expression Left;
        public readonly GreaterThanToken GreaterThan;
        public readonly Expression Right;

        public GreaterThanExpression(TokenCollection tokens, Expression left = null, GreaterThanToken? greaterThan = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            GreaterThan = greaterThan == null ? tokens.PopToken<GreaterThanToken>() : (GreaterThanToken)greaterThan;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += GreaterThan.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
