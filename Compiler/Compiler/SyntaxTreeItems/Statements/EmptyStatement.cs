using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class EmptyStatement : Statement
    {
        public readonly Token Semicolon;
        public EmptyStatement(TokenCollection tokens, Token semicolonToken)
        {
            Semicolon = semicolonToken;
        }
    }
}
