using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ExitStatement : Statement
    {
        public readonly ExitKeywordToken ExitKeyword;
        public readonly SemicolonToken? Semicolon;

        public override IToken LeftToken => ExitKeyword;
        public override IToken RightToken => Semicolon.RightToken;

        public ExitStatement(TokenCollection tokens, ExitKeywordToken? exitKeyword = null, SemicolonToken? semicolon = null)
        {
            ExitKeyword = exitKeyword == null ? tokens.PopToken<ExitKeywordToken>() : (ExitKeywordToken)exitKeyword;
            Semicolon = semicolon == null ? tokens.EnsureValidStatementEnding() : semicolon;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
