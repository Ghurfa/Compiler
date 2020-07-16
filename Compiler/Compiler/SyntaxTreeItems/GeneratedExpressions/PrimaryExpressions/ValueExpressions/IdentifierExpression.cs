using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class IdentifierExpression : PrimaryExpression
    {
        public IdentifierToken Identifier { get; private set; }

        public IdentifierExpression(TokenCollection tokens, IdentifierToken? identifier = null)
        {
            Identifier = identifier == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)identifier;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Identifier.ToString();
            return ret;
        }
    }
}
