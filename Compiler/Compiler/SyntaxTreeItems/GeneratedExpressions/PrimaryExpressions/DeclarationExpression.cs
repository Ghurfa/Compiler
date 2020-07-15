using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class DeclarationExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly IdentifierToken Identifier;
        public readonly ColonToken Colon;
        public readonly Type Type;

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
            ret += Colon.ToString();
            ret += Type.ToString();
            return ret;
        }
    }
}
