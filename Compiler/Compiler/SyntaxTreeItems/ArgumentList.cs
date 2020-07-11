using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ArgumentList
    {
        public readonly Argument[] Arguments;
        public ArgumentList(TokenCollection tokens)
        {
            var arguments = new LinkedList<Argument>();
            bool lastMissingComma = false;
            Token peek;
            while((peek = tokens.PeekToken()).Type != TokenType.ClosePeren)
            {
                if (lastMissingComma) throw new SyntaxTreeBuildingException(peek);
                var argument = new Argument(tokens);
                arguments.AddLast(argument);
                lastMissingComma = argument.CommaToken == null;
            }
            Arguments = arguments.ToArray();
        }
    }
}
