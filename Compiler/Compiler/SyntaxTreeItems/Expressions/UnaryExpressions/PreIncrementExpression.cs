using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.UnaryExpressions
{
    public class PreIncrementExpression : UnaryExpression
    {
        public readonly Token IncrementOperator;
        public readonly UnaryExpression Expression;
        public PreIncrementExpression(LinkedList<Token> tokens, Token incrOperator)
        {
            IncrementOperator = incrOperator;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }
    }
}
