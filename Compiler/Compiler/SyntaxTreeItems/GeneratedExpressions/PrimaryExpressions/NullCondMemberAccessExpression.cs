using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class NullCondMemberAccessExpression : PrimaryExpression
    {
        public PrimaryExpression BaseExpression { get; private set; }
        public NullCondDotToken NullCondDot { get; private set; }
        public IdentifierToken Item { get; private set; }

        public NullCondMemberAccessExpression(TokenCollection tokens, PrimaryExpression baseExpression = null, NullCondDotToken? nullCondDot = null, IdentifierToken? item = null)
        {
            BaseExpression = baseExpression == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : baseExpression;
            NullCondDot = nullCondDot == null ? tokens.PopToken<NullCondDotToken>() : (NullCondDotToken)nullCondDot;
            Item = item == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)item;
        }

        public override string ToString()
        {
            string ret = "";
            ret += BaseExpression.ToString();
            ret += " ";
            ret += NullCondDot.ToString();
            ret += " ";
            ret += Item.ToString();
            return ret;
        }
    }
}
