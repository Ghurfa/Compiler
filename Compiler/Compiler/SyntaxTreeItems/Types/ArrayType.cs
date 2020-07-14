using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Types
{
    public class ArrayType : Type
    {
        public readonly Type BaseType;
        public readonly IToken OpenArrayBracket;
        public readonly IToken CloseArrayBracket;
        public ArrayType(TokenCollection tokens, Type baseType)
        {
            BaseType = baseType;
            OpenArrayBracket = tokens.PopToken(TokenType.OpenBracket);
            CloseArrayBracket = tokens.PopToken(TokenType.CloseBracket);
        }
        public override string ToString()
        {
            return BaseType.ToString() + OpenArrayBracket.ToString() + CloseArrayBracket.ToString();
        }
    }
}
