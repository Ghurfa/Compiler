using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ContinueStatement : Statement
    {
        public readonly ContinueKeywordToken ContinueKeyword;
        public readonly SemicolonToken? Semicolon;

        public override IToken LeftToken => ContinueKeyword;
        public override IToken RightToken => Semicolon.RightToken;

        public ContinueStatement(TokenCollection tokens, ContinueKeywordToken? continueKeyword = null, SemicolonToken? semicolon = null)
        {
            ContinueKeyword = continueKeyword == null ? tokens.PopToken<ContinueKeywordToken>() : (ContinueKeywordToken)continueKeyword;
            Semicolon = semicolon == null ? tokens.EnsureValidStatementEnding() : semicolon;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
