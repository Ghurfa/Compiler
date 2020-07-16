using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PreIncrementExpression : UnaryExpression
    {
        public IncrementToken PreIncrement { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public PreIncrementExpression(TokenCollection tokens, IncrementToken? preIncrement = null, UnaryExpression expression = null)
        {
            PreIncrement = preIncrement == null ? tokens.PopToken<IncrementToken>() : (IncrementToken)preIncrement;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
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
