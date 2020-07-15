using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseNotExpression : UnaryExpression
    {
        public readonly BitwiseNotToken BitwiseNot;
        public readonly UnaryExpression Expression;

        public BitwiseNotExpression(TokenCollection tokens, BitwiseNotToken? bitwiseNot = null, UnaryExpression expression = null)
        {
            BitwiseNot = bitwiseNot == null ? tokens.PopToken<BitwiseNotToken>() : (BitwiseNotToken)bitwiseNot;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += BitwiseNot.ToString();
            ret += Expression.ToString();
            return ret;
        }
    }
}
