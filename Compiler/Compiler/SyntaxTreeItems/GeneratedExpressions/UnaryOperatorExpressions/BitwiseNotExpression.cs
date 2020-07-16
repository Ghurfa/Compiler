using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseNotExpression : UnaryExpression
    {
        public BitwiseNotToken BitwiseNot { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public BitwiseNotExpression(TokenCollection tokens, BitwiseNotToken? bitwiseNot = null, UnaryExpression expression = null)
        {
            BitwiseNot = bitwiseNot == null ? tokens.PopToken<BitwiseNotToken>() : (BitwiseNotToken)bitwiseNot;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += BitwiseNot.ToString();
            ret += " ";
            ret += Expression.ToString();
            return ret;
        }
    }
}
