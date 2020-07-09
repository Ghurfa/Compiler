using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ParameterDeclaration
    {
        public readonly Token Identifier;
        public readonly Token[] AccessModifiers;
        public readonly TypeToken TypeParameter;
        public readonly Token? CommaToken;
        public ParameterDeclaration(LinkedList<Token> tokens)
        {
            Identifier = tokens.GetToken(TokenType.Identifier);
            AccessModifiers = tokens.GetModifiers();
            TypeParameter = new TypeToken(tokens);
            if(tokens.PopIfMatches(out Token comma, TokenType.SyntaxChar, ","))
            {
                CommaToken = comma;
            }
        }
    }
}
