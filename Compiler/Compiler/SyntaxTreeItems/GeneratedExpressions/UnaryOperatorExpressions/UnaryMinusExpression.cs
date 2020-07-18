using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class UnaryMinusExpression : UnaryExpression
    {
        public MinusToken UnaryMinus { get; private set; }
        public UnaryExpression BaseExpression { get; private set; }

        public UnaryMinusExpression(TokenCollection tokens)
        {
            UnaryMinus = tokens.PopToken<MinusToken>();
            BaseExpression = UnaryExpression.ReadUnaryExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += UnaryMinus.ToString();
            ret += BaseExpression.ToString();
            return ret;
        }
    }
}
