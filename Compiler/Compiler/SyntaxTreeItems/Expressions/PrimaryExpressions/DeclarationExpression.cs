using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class DeclarationExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly Token Identifier;
        public readonly Token Colon;
        public readonly TypeToken TypeToken;

        public DeclarationExpression(TokenCollection tokens, Token identifier, Token colon)
        {
            Identifier = identifier;
            Colon = colon;
            TypeToken = new TypeToken(tokens);
        }
    }
}
