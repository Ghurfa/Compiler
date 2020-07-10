using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class PerenthesizedExpression : PrimaryExpression
    {
        public readonly Token OpenPerentheses;
        public readonly Expression Expression;
        public readonly Token ClosePerentheses;

        public PerenthesizedExpression(LinkedList<Token> tokens, Token openPeren)
        {
            OpenPerentheses = openPeren;
            Expression = ReadExpression(tokens);
            ClosePerentheses = tokens.GetToken(TokenType.SyntaxChar, ")");
        }
    }
}
