using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ArgumentList
    {
        public readonly Argument[] Arguments;

        public  IToken LeftToken => Arguments.First().LeftToken;
        public  IToken RightToken => Arguments.Last().RightToken;

        public ArgumentList(TokenCollection tokens, Argument[] arguments = null)
        {
            var argumentsList = new LinkedList<Argument>();
            if (arguments != null)
            {
                foreach (var item in arguments)
                {
                    argumentsList.AddLast(item);
                }
            }
            bool lastMissingComma = false;
            while(!lastMissingComma)
            {
                var newItem = new Argument(tokens);
                argumentsList.AddLast(newItem);
                lastMissingComma = newItem.Comma == null;
            }
            Arguments = argumentsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            return ret;
        }
    }
}
