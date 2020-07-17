using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class UnaryMinusExpression : UnaryExpression
    {
        public MinusToken UnaryMinus { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public UnaryMinusExpression(TokenCollection tokens)
        {
            UnaryMinus = tokens.PopToken<MinusToken>();
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
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
