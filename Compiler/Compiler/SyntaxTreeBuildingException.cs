using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class SyntaxTreeBuildingException : InvalidOperationException
    {
        public Token Token { get; private set; }
        public SyntaxTreeBuildingException()
        {
        }
        public SyntaxTreeBuildingException(Token token)
        {
            Token = token;
        }
    }
}
