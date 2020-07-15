using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class PrimitiveTypeExpression : PrimaryExpression
    {
        public readonly PrimitiveTypeToken PrimitiveType;

        public PrimitiveTypeExpression(TokenCollection tokens, PrimitiveTypeToken? primitiveType = null)
        {
            PrimitiveType = primitiveType == null ? tokens.PopToken<PrimitiveTypeToken>() : (PrimitiveTypeToken)primitiveType;
        }

        public override string ToString()
        {
            string ret = "";
            ret += PrimitiveType.ToString();
            return ret;
        }
    }
}
