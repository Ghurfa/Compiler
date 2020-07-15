using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class LessThanExpression : Expression
    {
        public readonly Expression Left;
        public readonly LessThanToken LessThan;
        public readonly Expression Right;

        public LessThanExpression(TokenCollection tokens, Expression left = null, LessThanToken? lessThan = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            LessThan = lessThan == null ? tokens.PopToken<LessThanToken>() : (LessThanToken)lessThan;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += LessThan.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
