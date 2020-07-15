using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class EmptyStatement : Statement
    {
        public readonly SemicolonToken Semicolon;
        public EmptyStatement(TokenCollection tokens)
        {
            Semicolon = tokens.PopToken<SemicolonToken>();
        }
    }
}
