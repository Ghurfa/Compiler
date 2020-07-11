using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.UnaryExpressions
{
    public class DereferenceExpression : UnaryExpression
    {
        public readonly Token DereferenceOperator;
        public readonly UnaryExpression Expression;
        public DereferenceExpression(TokenCollection tokens, Token derefOperator)
        {
            DereferenceOperator = derefOperator;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }
    }
}
