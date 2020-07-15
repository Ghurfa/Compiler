using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class BreakStatement : Statement
    {
        public readonly BreakKeywordToken BreakKeyword;
        public readonly SemicolonToken? Semicolon;

        public override IToken LeftToken => BreakKeyword;
        public override IToken RightToken => Semicolon.RightToken;

        public BreakStatement(TokenCollection tokens, BreakKeywordToken? breakKeyword = null, SemicolonToken? semicolon = null)
        {
            BreakKeyword = breakKeyword == null ? tokens.PopToken<BreakKeywordToken>() : (BreakKeywordToken)breakKeyword;
            Semicolon = semicolon == null ? tokens.EnsureValidStatementEnding() : semicolon;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
