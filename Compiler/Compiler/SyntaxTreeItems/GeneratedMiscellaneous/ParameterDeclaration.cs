using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ParameterDeclaration
    {
        public IdentifierToken Identifier { get; private set; }
        public ColonToken Colon { get; private set; }
        public Type Type { get; private set; }
        public CommaToken? Comma { get; private set; }

        public ParameterDeclaration(TokenCollection tokens)
        {
            Identifier = tokens.PopToken<IdentifierToken>();
            Colon = tokens.PopToken<ColonToken>();
            Type = Type.ReadType(tokens);
            if (tokens.PopIfMatches(out CommaToken comma))
            {
                Comma = comma;
            }
        }

        public override string ToString()
        {
            string ret = "";
            ret += Identifier.ToString();
            ret += Colon.ToString();
            ret += Type.ToString();
            ret += Comma?.ToString();
            return ret;
        }
    }
}
