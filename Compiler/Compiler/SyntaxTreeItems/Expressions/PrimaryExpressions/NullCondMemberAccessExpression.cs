using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class NullCondMemberAccessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly NullCondDotToken NullCondDot;
        public readonly IdentifierToken Item;

        public override IToken LeftToken => BaseExpression.LeftToken;
        public override IToken RightToken => Item;

        public NullCondMemberAccessExpression(TokenCollection tokens, PrimaryExpression baseExpression = null, NullCondDotToken? nullCondDot = null, IdentifierToken? item = null)
        {
            BaseExpression = baseExpression == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : baseExpression;
            NullCondDot = nullCondDot == null ? tokens.PopToken<NullCondDotToken>() : (NullCondDotToken)nullCondDot;
            Item = item == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)item;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
