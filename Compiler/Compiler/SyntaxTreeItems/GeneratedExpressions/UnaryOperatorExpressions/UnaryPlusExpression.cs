using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class UnaryPlusExpression : UnaryExpression
    {
        public PlusToken UnaryPlus { get; private set; }
        public UnaryExpression BaseExpression { get; private set; }

        public UnaryPlusExpression(TokenCollection tokens)
        {
            UnaryPlus = tokens.PopToken<PlusToken>();
            BaseExpression = UnaryExpression.ReadUnaryExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += UnaryPlus.ToString();
            ret += BaseExpression.ToString();
            return ret;
        }
    }
}
