using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class MemberAccessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly Token Dot;
        public readonly Token Item;

        public MemberAccessExpression(TokenCollection tokens, PrimaryExpression baseExpression)
        {
            BaseExpression = baseExpression;
            Dot = tokens.PopToken(TokenType.Dot);
            Item = tokens.PopToken(TokenType.Identifier);
        }
        public override string ToString()
        {
            return BaseExpression.ToString() + Dot.ToString() + Item.ToString();
        }
    }
}
