using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.UnaryExpressions
{
    public class UnaryPlusExpression : UnaryExpression
    {
        public readonly Token PlusOperator;
        public readonly UnaryExpression Expression;
        public UnaryPlusExpression(LinkedList<Token> tokens, Token plusOperator)
        {
            PlusOperator = plusOperator;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }
    }
}
