using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class PostDecrementExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly DecrementToken Decrement;

        public override IToken LeftToken => BaseExpression.LeftToken;
        public override IToken RightToken => Decrement;

        public PostDecrementExpression(TokenCollection tokens, PrimaryExpression baseExpression = null, DecrementToken? decrement = null)
        {
            BaseExpression = baseExpression == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : baseExpression;
            Decrement = decrement == null ? tokens.PopToken<DecrementToken>() : (DecrementToken)decrement;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
