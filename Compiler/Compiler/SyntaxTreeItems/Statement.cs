using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public abstract class Statement
    {
        public static Statement ReadStatement(LinkedList<Token> tokens)
        {
            throw new NotImplementedException();
        }
    }
}
