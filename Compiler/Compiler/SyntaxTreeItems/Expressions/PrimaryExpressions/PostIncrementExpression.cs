using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class PostIncrementExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly IncrementToken Increment;

        public override IToken LeftToken => BaseExpression.LeftToken;
        public override IToken RightToken => Increment;

        public PostIncrementExpression(TokenCollection tokens, PrimaryExpression baseExpression = null, IncrementToken? increment = null)
        {
            BaseExpression = baseExpression == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : baseExpression;
            Increment = increment == null ? tokens.PopToken<IncrementToken>() : (IncrementToken)increment;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
