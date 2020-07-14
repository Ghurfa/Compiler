using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Types
{
    public class BuiltInType : Type
    {
        public readonly IToken TypeToken;

        public BuiltInType(TokenCollection tokens)
        {
            TypeToken = tokens.PopToken(TokenType.PrimitiveType);
        }
        public override string ToString()
        {
            return TypeToken.ToString();
        }
    }
}
