using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PreIncrementExpression : UnaryExpression
    {
        public IncrementToken PreIncrement { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public PreIncrementExpression(TokenCollection tokens)
        {
            PreIncrement = tokens.PopToken<IncrementToken>();
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += PreIncrement.ToString();
            ret += Expression.ToString();
            return ret;
        }
    }
}
