using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class OrExpression : Expression
    {
        public readonly Expression Left;
        public readonly OrToken Or;
        public readonly Expression Right;

        public OrExpression(TokenCollection tokens, Expression left = null, OrToken? or = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Or = or == null ? tokens.PopToken<OrToken>() : (OrToken)or;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += Or.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
