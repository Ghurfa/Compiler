using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PreDecrementExpression : UnaryExpression
    {
        public DecrementToken PreDecrement { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public PreDecrementExpression(TokenCollection tokens)
        {
            PreDecrement = tokens.PopToken<DecrementToken>();
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += PreDecrement.ToString();
            ret += Expression.ToString();
            return ret;
        }
    }
}
