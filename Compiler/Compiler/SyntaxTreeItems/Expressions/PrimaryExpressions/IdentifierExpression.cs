using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class IdentifierExpression : PrimaryExpression
    {
        public readonly IToken Identifier;
        public IdentifierExpression(TokenCollection tokens, IToken identifier)
        {
            Identifier = identifier;
        }
        public override string ToString()
        {
            return Identifier.ToString();
        }
    }
}
