using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class NullCondMemberAccessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly IToken NullCondDot;
        public readonly IToken Item;

        public NullCondMemberAccessExpression(TokenCollection tokens, PrimaryExpression baseExpression)
        {
            BaseExpression = baseExpression;
            NullCondDot = tokens.PopToken(TokenType.NullCondDot);
            Item = tokens.PopToken(TokenType.Identifier);
        }
        public override string ToString()
        {
            return BaseExpression.ToString() + NullCondDot.ToString() + Item.ToString();
        }
    }
}
