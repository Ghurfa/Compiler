using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class FalseLiteralExpression : PrimaryExpression
    {
        public FalseKeywordToken FalseKeyword { get; private set; }

        public FalseLiteralExpression(TokenCollection tokens, FalseKeywordToken? falseKeyword = null)
        {
            FalseKeyword = falseKeyword == null ? tokens.PopToken<FalseKeywordToken>() : (FalseKeywordToken)falseKeyword;
        }

        public override string ToString()
        {
            string ret = "";
            ret += FalseKeyword.ToString();
            return ret;
        }
    }
}
