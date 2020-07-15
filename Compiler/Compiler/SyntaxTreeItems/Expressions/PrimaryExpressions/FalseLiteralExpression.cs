using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class FalseLiteralExpression : PrimaryExpression
    {
        public readonly FalseKeywordToken FalseKeyword;

        public override IToken LeftToken => FalseKeyword;
        public override IToken RightToken => FalseKeyword;

        public FalseLiteralExpression(TokenCollection tokens, FalseKeywordToken? falseKeyword = null)
        {
            FalseKeyword = falseKeyword == null ? tokens.PopToken<FalseKeywordToken>() : (FalseKeywordToken)falseKeyword;
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
