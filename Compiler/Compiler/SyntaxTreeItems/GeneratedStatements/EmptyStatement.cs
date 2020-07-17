using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class EmptyStatement : Statement
    {
        public SemicolonToken Semicolon { get; private set; }

        public EmptyStatement(TokenCollection tokens)
        {
            Semicolon = tokens.PopToken<SemicolonToken>();
        }

        public override string ToString()
        {
            string ret = "";
            ret += Semicolon.ToString();
            return ret;
        }
    }
}
