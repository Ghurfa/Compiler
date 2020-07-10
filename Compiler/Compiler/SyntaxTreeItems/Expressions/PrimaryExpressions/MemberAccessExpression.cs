using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class MemberAccessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly Token DotToken;
        public readonly Token ItemToken;

        public MemberAccessExpression(LinkedList<Token> tokens, PrimaryExpression baseExpression, Token dotToken)
        {
            BaseExpression = baseExpression;
            DotToken = dotToken;
            ItemToken = tokens.GetToken(TokenType.Identifier);
        }
    }
}
