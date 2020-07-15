using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class MemberAccessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly DotToken Dot;
        public readonly IdentifierToken Item;

        public override IToken LeftToken => BaseExpression.LeftToken;
        public override IToken RightToken => Item;

        public MemberAccessExpression(TokenCollection tokens, PrimaryExpression baseExpression = null, DotToken? dot = null, IdentifierToken? item = null)
        {
            BaseExpression = baseExpression == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : baseExpression;
            Dot = dot == null ? tokens.PopToken<DotToken>() : (DotToken)dot;
            Item = item == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)item;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
