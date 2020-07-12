using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class PostDecrementExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly Token Decrement;

        public PostDecrementExpression(TokenCollection tokens, PrimaryExpression baseExpr)
        {
            BaseExpression = baseExpr;
            Decrement = tokens.PopToken(TokenType.Decrement);
        }
        public override string ToString()
        {
            return BaseExpression.ToString() + Decrement.ToString();
        }
    }
}
