using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class IdentifierExpression : PrimaryExpression
    {
        public readonly Token Identifier;
        public IdentifierExpression(TokenCollection tokens, Token identifier)
        {
            Identifier = identifier;
        }
    }
}
