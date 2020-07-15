using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ParameterDeclaration
    {
        public readonly IdentifierToken Identifier;
        public readonly ColonToken Colon;
        public readonly Type Type;
        public readonly CommaToken Comma;

        public  IToken LeftToken => Identifier;
        public  IToken RightToken => Comma;

        public ParameterDeclaration(TokenCollection tokens, IdentifierToken? identifier = null, ColonToken? colon = null, Type type = null, CommaToken? comma = null)
        {
            Identifier = identifier == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)identifier;
            Colon = colon == null ? tokens.PopToken<ColonToken>() : (ColonToken)colon;
            Type = type == null ? Type.ReadType(tokens) : type;
            Comma = comma == null ? tokens.PopToken<CommaToken>() : (CommaToken)comma;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            
            return ret;
        }
    }
}
