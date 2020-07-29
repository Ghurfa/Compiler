using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class ArgumentList
    {
        public Argument[] Arguments { get; private set; }

        public ArgumentList(TokenCollection tokens)
        {
            LinkedList<Argument> argumentsList = new LinkedList<Argument>();
            bool lastMissingComma = tokens.PeekToken() is ClosePerenToken;
            while (!lastMissingComma)
            {
                var add = new Argument(tokens);
                argumentsList.AddLast(add);
                lastMissingComma = add.Comma == null;
            }
            Arguments = argumentsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            foreach (var item in Arguments)
            {
                ret += item.ToString();
            }
            return ret;
        }
    }
}
