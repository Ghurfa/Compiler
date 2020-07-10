using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ParameterDeclaration
    {
        public readonly Token Identifier;
        public readonly Token ColonToken;
        public readonly TypeToken TypeParameter;
        public readonly Token? CommaToken;
        public ParameterDeclaration(TokenCollection tokens)
        {
            Identifier = tokens.PopToken(TokenType.Identifier);
            ColonToken = tokens.PopToken(TokenType.SyntaxChar, ":");
            TypeParameter = new TypeToken(tokens);
            if(tokens.PopIfMatches(out Token comma, TokenType.SyntaxChar, ","))
            {
                CommaToken = comma; 
            }
        }
    }
}
