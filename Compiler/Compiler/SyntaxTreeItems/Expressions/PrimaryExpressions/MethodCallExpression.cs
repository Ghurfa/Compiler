using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class MethodCallExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly PrimaryExpression MethodExpression;
        public readonly IToken OpenPeren;
        public readonly ArgumentList Arguments;
        public readonly IToken ClosePeren;

        public MethodCallExpression(TokenCollection tokens, PrimaryExpression baseExpr)
        {
            MethodExpression = baseExpr;
            OpenPeren = tokens.PopToken(TokenType.OpenPeren);
            Arguments = new ArgumentList(tokens);
            ClosePeren = tokens.PopToken(TokenType.ClosePeren);
        }
        public override string ToString()
        {
            return MethodExpression.ToString() + OpenPeren.ToString() + Arguments.ToString() + ClosePeren.ToString();
        }
    }
}
