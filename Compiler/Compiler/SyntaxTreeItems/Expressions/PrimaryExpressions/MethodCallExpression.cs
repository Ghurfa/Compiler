using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class MethodCallExpression : PrimaryExpression
    {
        public readonly PrimaryExpression MethodExpression;
        public readonly Token OpenPerenthesesToken;
        public readonly ArgumentList Arguments;
        public readonly Token ClosePerenthesesToken;

        public MethodCallExpression(LinkedList<Token> tokens, PrimaryExpression baseExpr, Token openPeren)
        {
            MethodExpression = baseExpr;
            OpenPerenthesesToken = openPeren;
            Arguments = new ArgumentList(tokens);
            ClosePerenthesesToken = tokens.GetToken(TokenType.SyntaxChar, ")");
        }
    }
}
