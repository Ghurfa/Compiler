using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Types
{
    public class PrimitiveType : Type
    {
        public readonly PrimitiveTypeToken TypeToken;

        public PrimitiveType(TokenCollection tokens)
        {
            TypeToken = tokens.PopToken<PrimitiveTypeToken>();
        }
        public override string ToString()
        {
            return TypeToken.ToString();
        }
    }
}
