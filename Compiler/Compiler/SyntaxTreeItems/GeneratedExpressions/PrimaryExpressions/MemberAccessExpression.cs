using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MemberAccessExpression : PrimaryExpression
    {
        public PrimaryExpression BaseExpression { get; private set; }
        public DotToken Dot { get; private set; }
        public IdentifierToken Item { get; private set; }

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
