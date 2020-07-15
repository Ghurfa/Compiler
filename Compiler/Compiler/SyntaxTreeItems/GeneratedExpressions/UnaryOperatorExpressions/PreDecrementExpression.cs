using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class PreDecrementExpression : UnaryExpression
    {
        public readonly DecrementToken PreDecrement;
        public readonly UnaryExpression Expression;

        public PreDecrementExpression(TokenCollection tokens, DecrementToken? preDecrement = null, UnaryExpression expression = null)
        {
            PreDecrement = preDecrement == null ? tokens.PopToken<DecrementToken>() : (DecrementToken)preDecrement;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
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
