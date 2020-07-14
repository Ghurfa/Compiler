using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class DeclarationExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly IToken Identifier;
        public readonly IToken Colon;
        public readonly Type Type;

        public DeclarationExpression(TokenCollection tokens, IToken identifier, IToken colon)
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
