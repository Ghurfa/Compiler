using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PrimitiveTypeExpression : PrimaryExpression
    {
        public PrimitiveTypeToken PrimitiveType { get; private set; }

        public PrimitiveTypeExpression(TokenCollection tokens, PrimitiveTypeToken primitiveType)
        {
            PrimitiveType = primitiveType;
        }

        public override string ToString()
        {
            string ret = "";
            ret += PrimitiveType.ToString();
            return ret;
        }
    }
}
