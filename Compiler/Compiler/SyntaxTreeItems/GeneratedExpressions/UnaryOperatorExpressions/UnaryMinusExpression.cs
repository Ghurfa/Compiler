using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class UnaryMinusExpression : UnaryExpression
    {
        public MinusToken UnaryMinus { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public UnaryMinusExpression(TokenCollection tokens, MinusToken? unaryMinus = null, UnaryExpression expression = null)
        {
            UnaryMinus = unaryMinus == null ? tokens.PopToken<MinusToken>() : (MinusToken)unaryMinus;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += UnaryMinus.ToString();
            ret += " ";
            ret += Expression.ToString();
            return ret;
        }
    }
}
