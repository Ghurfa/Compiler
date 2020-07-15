using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class DivideExpression : Expression
    {
        public readonly Expression Left;
        public readonly DivideToken Divide;
        public readonly Expression Right;

        public DivideExpression(TokenCollection tokens, Expression left = null, DivideToken? divide = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Divide = divide == null ? tokens.PopToken<DivideToken>() : (DivideToken)divide;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += Divide.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
