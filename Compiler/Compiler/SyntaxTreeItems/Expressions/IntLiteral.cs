using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class IntLiteral : Expression
    {
        public readonly Token Token;

        public IntLiteral(LinkedList<Token> tokens, Token token)
        {
            Token = token;
        }
    }
}
