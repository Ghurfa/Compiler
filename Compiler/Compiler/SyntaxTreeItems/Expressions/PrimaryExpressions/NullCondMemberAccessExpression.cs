using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class NullCondMemberAccessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly Token NullCondDot;
        public readonly Token Item;

        public NullCondMemberAccessExpression(TokenCollection tokens, PrimaryExpression baseExpression)
        {
            BaseExpression = baseExpression;
            NullCondDot = tokens.PopToken(TokenType.NullCondDot);
            Item = tokens.PopToken(TokenType.Identifier);
        }
    }
}
