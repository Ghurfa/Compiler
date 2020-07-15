using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class MemberAccessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly DotToken Dot;
        public readonly IdentifierToken Item;

        public MemberAccessExpression(TokenCollection tokens, PrimaryExpression baseExpression = null, DotToken? dot = null, IdentifierToken? item = null)
        {
            BaseExpression = baseExpression == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : baseExpression;
            Dot = dot == null ? tokens.PopToken<DotToken>() : (DotToken)dot;
            Item = item == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)item;
        }

        public override string ToString()
        {
            string ret = "";
            ret += BaseExpression.ToString();
            ret += Dot.ToString();
            ret += Item.ToString();
            return ret;
        }
    }
}
