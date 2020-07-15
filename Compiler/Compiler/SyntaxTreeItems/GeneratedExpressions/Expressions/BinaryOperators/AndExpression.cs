using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class AndExpression : Expression
    {
        public readonly Expression Left;
        public readonly AndToken And;
        public readonly Expression Right;

        public AndExpression(TokenCollection tokens, Expression left = null, AndToken? and = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            And = and == null ? tokens.PopToken<AndToken>() : (AndToken)and;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += And.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
