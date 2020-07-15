using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class FalseLiteralExpression : PrimaryExpression
    {
        public readonly FalseKeywordToken FalseKeyword;

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
