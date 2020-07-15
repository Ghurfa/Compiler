using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class IdentifierExpression : PrimaryExpression
    {
        public readonly IdentifierToken Identifier;

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
