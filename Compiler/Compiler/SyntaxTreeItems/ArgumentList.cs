using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ArgumentList
    {
        public readonly Argument[] Arguments;
        public ArgumentList(LinkedList<Token> tokens)
        {
            var arguments = new LinkedList<Argument>();
            bool lastMissingComma = false;
            while(!lastMissingComma)
            {
                if (lastMissingComma) throw new SyntaxTreeBuildingException(tokens.First.Value);
                var argument = new Argument(tokens);
                arguments.AddLast(argument);
                lastMissingComma = argument.CommaToken == null;
            }
        }
    }
}
