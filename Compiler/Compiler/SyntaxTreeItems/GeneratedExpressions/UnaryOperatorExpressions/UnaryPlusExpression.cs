using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class UnaryPlusExpression : UnaryExpression
    {
        public PlusToken UnaryPlus { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public UnaryPlusExpression(TokenCollection tokens, PlusToken? unaryPlus = null, UnaryExpression expression = null)
        {
            UnaryPlus = unaryPlus == null ? tokens.PopToken<PlusToken>() : (PlusToken)unaryPlus;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += UnaryPlus.ToString();
            ret += " ";
            ret += Expression.ToString();
            return ret;
        }
    }
}
