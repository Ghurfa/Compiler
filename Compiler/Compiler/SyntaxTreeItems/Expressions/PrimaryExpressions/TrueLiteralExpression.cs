using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class TrueLiteralExpression : PrimaryExpression
    {
        public readonly TrueKeywordToken TrueKeyword;

        public override IToken LeftToken => TrueKeyword;
        public override IToken RightToken => TrueKeyword;

        public TrueLiteralExpression(TokenCollection tokens, TrueKeywordToken? trueKeyword = null)
        {
            TrueKeyword = trueKeyword == null ? tokens.PopToken<TrueKeywordToken>() : (TrueKeywordToken)trueKeyword;
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
