using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class UnaryMinusExpression : UnaryExpression
    {
        public readonly MinusToken UnaryMinus;
        public readonly UnaryExpression Expression;

        public UnaryMinusExpression(TokenCollection tokens, MinusToken? unaryMinus = null, UnaryExpression expression = null)
        {
            UnaryMinus = unaryMinus == null ? tokens.PopToken<MinusToken>() : (MinusToken)unaryMinus;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += UnaryMinus.ToString();
            ret += Expression.ToString();
            return ret;
        }
    }
}
