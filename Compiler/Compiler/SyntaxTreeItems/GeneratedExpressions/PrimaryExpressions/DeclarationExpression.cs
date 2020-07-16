using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class DeclarationExpression : PrimaryExpression, ICompleteStatement
    {
        public IdentifierToken Identifier { get; private set; }
        public ColonToken Colon { get; private set; }
        public Type Type { get; private set; }

        public DeclarationExpression(TokenCollection tokens, IdentifierToken? identifier = null, ColonToken? colon = null, Type type = null)
        {
            Identifier = identifier == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)identifier;
            Colon = colon == null ? tokens.PopToken<ColonToken>() : (ColonToken)colon;
            Type = type == null ? Type.ReadType(tokens) : type;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Identifier.ToString();
            ret += " ";
            ret += Colon.ToString();
            ret += " ";
            ret += Type.ToString();
            return ret;
        }
    }
}
