using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class NotEqualsExpression : Expression
    {
        public readonly Expression Left;
        public readonly NotEqualsToken NotEquals;
        public readonly Expression Right;

        public NotEqualsExpression(TokenCollection tokens, Expression left = null, NotEqualsToken? notEquals = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            NotEquals = notEquals == null ? tokens.PopToken<NotEqualsToken>() : (NotEqualsToken)notEquals;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += NotEquals.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
