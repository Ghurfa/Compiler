using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public abstract class PostIncrementDecrementExpressions : PrimaryExpression
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly Token IncrementDecrement;

        public PostIncrementDecrementExpressions(TokenCollection tokens, PrimaryExpression baseExpr, Token incrDecr)
        {
            BaseExpression = baseExpr;
            IncrementDecrement = incrDecr;
        }
    }
    public class PostIncrementExpression : PostIncrementDecrementExpressions
    {
        public PostIncrementExpression(TokenCollection tokens, PrimaryExpression baseExpr, Token incrDecr) : base(tokens, baseExpr, incrDecr) { }
    }
    public class PostDecrementExpression : PostIncrementDecrementExpressions
    {
        public PostDecrementExpression(TokenCollection tokens, PrimaryExpression baseExpr, Token incrDecr) : base(tokens, baseExpr, incrDecr) { }
    }
}
