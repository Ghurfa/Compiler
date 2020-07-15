using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ArrayType : Type
    {
        public readonly Type BaseType;
        public readonly OpenBracketToken OpenBracket;
        public readonly CloseBracketToken CloseBracket;

        public override IToken LeftToken => BaseType.LeftToken;
        public override IToken RightToken => CloseBracket;

        public ArrayType(TokenCollection tokens, Type baseType = null, OpenBracketToken? openBracket = null, CloseBracketToken? closeBracket = null)
        {
            BaseType = baseType == null ? Type.ReadType(tokens) : baseType;
            OpenBracket = openBracket == null ? tokens.PopToken<OpenBracketToken>() : (OpenBracketToken)openBracket;
            CloseBracket = closeBracket == null ? tokens.PopToken<CloseBracketToken>() : (CloseBracketToken)closeBracket;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
