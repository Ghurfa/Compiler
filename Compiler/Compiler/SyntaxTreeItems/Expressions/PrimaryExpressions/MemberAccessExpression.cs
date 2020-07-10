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

        public MemberAccessExpression(TokenCollection tokens, PrimaryExpression baseExpression, Token dotToken)
        {
            BaseExpression = baseExpression;
            DotToken = dotToken;
            ItemToken = tokens.PopToken(TokenType.Identifier);
        }
    }
}
