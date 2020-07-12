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
                if (lastMissingComma) throw new UnexpectedToken(peek);
                var argument = new Argument(tokens);
                arguments.AddLast(argument);
                lastMissingComma = argument.CommaToken == null;
            }
            Arguments = arguments.ToArray();
        }
    }
    public class Argument
    {
        public readonly Expression Expression;
        public readonly Token? CommaToken;

        public Argument(TokenCollection tokens)
        {
            Expression = Expression.ReadExpression(tokens);
            if (tokens.PopIfMatches(out Token comma, TokenType.Comma))
            {
                CommaToken = comma;
            }
        }
    }
}
