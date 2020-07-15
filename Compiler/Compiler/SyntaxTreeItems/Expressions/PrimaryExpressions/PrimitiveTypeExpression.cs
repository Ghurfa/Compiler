using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class PrimitiveTypeExpression : PrimaryExpression
    {
        public readonly PrimitiveTypeToken PrimitiveType;

        public override IToken LeftToken => PrimitiveType;
        public override IToken RightToken => PrimitiveType;

        public PrimitiveTypeExpression(TokenCollection tokens, PrimitiveTypeToken? primitiveType = null)
        {
            PrimitiveType = primitiveType == null ? tokens.PopToken<PrimitiveTypeToken>() : (PrimitiveTypeToken)primitiveType;
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
