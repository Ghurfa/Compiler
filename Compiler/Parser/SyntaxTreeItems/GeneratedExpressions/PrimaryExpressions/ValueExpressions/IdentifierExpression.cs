using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class IdentifierExpression : PrimaryExpression, IAssignableExpression
    {
        public IdentifierToken Identifier { get; private set; }

        public IdentifierExpression(TokenCollection tokens, IdentifierToken identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Identifier.ToString();
            return ret;
        }
    }
}
