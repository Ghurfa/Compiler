using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ParameterDeclaration
    {
        public readonly IToken Identifier;
        public readonly IToken ColonToken;
        public readonly Type TypeParameter;
        public readonly IToken? CommaToken;
        public ParameterDeclaration(TokenCollection tokens)
        {
            Identifier = tokens.PopToken(TokenType.Identifier);
            ColonToken = tokens.PopToken(TokenType.Colon);
            TypeParameter = Type.ReadType(tokens);
            if (tokens.PopIfMatches(out IToken comma, TokenType.Comma))
            {
                CommaToken = comma;
            }
        }
    }
}
