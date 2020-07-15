using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class PrimitiveType : Type
    {
        public readonly PrimitiveTypeToken PrimitiveTypeKeyword;

        public override IToken LeftToken => PrimitiveTypeKeyword;
        public override IToken RightToken => PrimitiveTypeKeyword;

        public PrimitiveType(TokenCollection tokens, PrimitiveTypeToken? primitiveTypeKeyword = null)
        {
            PrimitiveTypeKeyword = primitiveTypeKeyword == null ? tokens.PopToken<PrimitiveTypeToken>() : (PrimitiveTypeToken)primitiveTypeKeyword;
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
