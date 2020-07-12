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
                if (lastMissingComma) throw new InvalidTokenException(peek);
                var argument = new Argument(tokens);
                arguments.AddLast(argument);
                lastMissingComma = argument.CommaToken == null;
            }
            Arguments = arguments.ToArray();
        }
        public override string ToString()
        {
            string ret = "";
            for(int i = 0; i < Arguments.Length; i++)
            {
                ret += Arguments[i].ToString();
                if (i < Arguments.Length - 1) ret += " ";
            }
            return ret;
        }
    }
}
