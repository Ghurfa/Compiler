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
        public ParameterDeclaration(LinkedList<Token> tokens)
        {
            Identifier = tokens.GetToken(TokenType.Identifier);
            ColonToken = tokens.GetToken(TokenType.SyntaxChar, ":");
            TypeParameter = new TypeToken(tokens);
            if(tokens.PopIfMatches(out Token comma, TokenType.SyntaxChar, ","))
            {
                CommaToken = comma; 
            }
        }
    }
}
