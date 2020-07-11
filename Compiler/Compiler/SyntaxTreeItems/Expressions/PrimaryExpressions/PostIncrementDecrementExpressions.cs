using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class PostIncrementExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly Token Increment;

        public PostIncrementExpression(TokenCollection tokens, PrimaryExpression baseExpr)
        {
            BaseExpression = baseExpr;
            Increment = tokens.PopToken(TokenType.Increment);
        }
    }
    public class PostDecrementExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly Token Decrement;

        public PostDecrementExpression(TokenCollection tokens, PrimaryExpression baseExpr)
        {
            BaseExpression = baseExpr;
            Decrement = tokens.PopToken(TokenType.Decrement);
        }
    }
}
