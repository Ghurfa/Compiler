using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PreDecrementExpression : UnaryExpression
    {
        public DecrementToken PreDecrement { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public PreDecrementExpression(TokenCollection tokens, DecrementToken? preDecrement = null, UnaryExpression expression = null)
        {
            PreDecrement = preDecrement == null ? tokens.PopToken<DecrementToken>() : (DecrementToken)preDecrement;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += PreDecrement.ToString();
            ret += " ";
            ret += Expression.ToString();
            return ret;
        }
    }
}
