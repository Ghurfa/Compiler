using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseNotExpression : UnaryExpression
    {
        public BitwiseNotToken BitwiseNot { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public BitwiseNotExpression(TokenCollection tokens)
        {
            BitwiseNot = tokens.PopToken<BitwiseNotToken>();;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
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
