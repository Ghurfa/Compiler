using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ParameterDeclaration
    {
        public readonly IdentifierToken Identifier;
        public readonly ColonToken Colon;
        public readonly Type TypeParameter;
        public readonly CommaToken? Comma;
        public ParameterDeclaration(TokenCollection tokens)
        {
            Identifier = tokens.PopToken<IdentifierToken>();
            Colon = tokens.PopToken<ColonToken>();
            TypeParameter = Type.ReadType(tokens);
            if (tokens.PopIfMatches(out CommaToken comma))
            {
                Comma = comma;
            }
        }
    }
}
