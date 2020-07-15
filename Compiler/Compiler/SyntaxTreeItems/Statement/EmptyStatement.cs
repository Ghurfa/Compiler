using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class EmptyStatement : Statement
    {
        public readonly SemicolonToken Semicolon;

        public override IToken LeftToken => Semicolon;
        public override IToken RightToken => Semicolon;

        public EmptyStatement(TokenCollection tokens, SemicolonToken? semicolon = null)
        {
            Semicolon = semicolon == null ? tokens.PopToken<SemicolonToken>() : (SemicolonToken)semicolon;
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
