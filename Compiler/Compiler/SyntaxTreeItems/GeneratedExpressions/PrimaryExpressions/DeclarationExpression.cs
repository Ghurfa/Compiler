using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class DeclarationExpression : PrimaryExpression, IAssignableExpression, ICompleteStatement
    {
        public IdentifierToken Identifier { get; private set; }
        public ColonToken Colon { get; private set; }
        public Type Type { get; private set; }

        public DeclarationExpression(TokenCollection tokens, IdentifierToken identifier, ColonToken colon)
        {
            Identifier = identifier;
            Colon = colon;
            Type = Type.ReadType(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Identifier.ToString();
            ret += Colon.ToString();
            ret += Type.ToString();
            return ret;
        }
    }
}
