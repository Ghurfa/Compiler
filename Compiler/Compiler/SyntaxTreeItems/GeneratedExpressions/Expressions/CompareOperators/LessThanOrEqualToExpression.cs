using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class LessThanOrEqualToExpression : Expression
    {
        public readonly Expression Left;
        public readonly LessThanOrEqualToToken LessThanOrEqualTo;
        public readonly Expression Right;

        public LessThanOrEqualToExpression(TokenCollection tokens, Expression left = null, LessThanOrEqualToToken? lessThanOrEqualTo = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            LessThanOrEqualTo = lessThanOrEqualTo == null ? tokens.PopToken<LessThanOrEqualToToken>() : (LessThanOrEqualToToken)lessThanOrEqualTo;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += LessThanOrEqualTo.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
