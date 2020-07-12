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
        public readonly Type Type;

        public DeclarationExpression(TokenCollection tokens, Token identifier, Token colon)
        {
            Identifier = identifier;
            Colon = colon;
            Type = Type.ReadType(tokens);
        }
        public override string ToString()
        {
            return Identifier.ToString() + Colon.ToString() + Type.ToString();
        }
    }
}
