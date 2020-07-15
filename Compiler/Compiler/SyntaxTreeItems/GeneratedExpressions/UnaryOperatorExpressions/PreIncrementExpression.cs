using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class PreIncrementExpression : UnaryExpression
    {
        public readonly IncrementToken PreIncrement;
        public readonly UnaryExpression Expression;

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
