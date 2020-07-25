using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ExitStatement : Statement
    {
        public ExitKeywordToken ExitKeyword { get; private set; }
        public SemicolonToken? Semicolon { get; private set; }

        public ExitStatement(TokenCollection tokens)
        {
            ExitKeyword = tokens.PopToken<ExitKeywordToken>();
            Semicolon = tokens.EnsureValidStatementEnding();
        }

        public override string ToString()
        {
            string ret = "";
            ret += ExitKeyword.ToString();
            ret += Semicolon?.ToString();
            return ret;
        }
    }
}
