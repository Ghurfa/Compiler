using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class UnaryPlusExpression : UnaryExpression
    {
        public readonly PlusToken UnaryPlus;
        public readonly UnaryExpression Expression;

        public UnaryPlusExpression(TokenCollection tokens, PlusToken? unaryPlus = null, UnaryExpression expression = null)
        {
            UnaryPlus = unaryPlus == null ? tokens.PopToken<PlusToken>() : (PlusToken)unaryPlus;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += UnaryPlus.ToString();
            ret += Expression.ToString();
            return ret;
        }
    }
}
