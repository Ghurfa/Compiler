using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class DeclarationExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly IdentifierToken Identifier;
        public readonly ColonToken Colon;
        public readonly Type Type;

        public override IToken LeftToken => Identifier;
        public override IToken RightToken => Type.RightToken;

        public DeclarationExpression(TokenCollection tokens, IdentifierToken? identifier = null, ColonToken? colon = null, Type type = null)
        {
            Identifier = identifier == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)identifier;
            Colon = colon == null ? tokens.PopToken<ColonToken>() : (ColonToken)colon;
            Type = type == null ? Type.ReadType(tokens) : type;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
