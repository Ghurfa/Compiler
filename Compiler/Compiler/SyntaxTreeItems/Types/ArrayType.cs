using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Types
{
    public class ArrayType : Type
    {
        public readonly Type BaseType;
        public readonly OpenBracketToken OpenBracket;
        public readonly CloseBracketToken CloseBracket;
        public ArrayType(TokenCollection tokens, Type baseType)
        {
            BaseType = baseType;
            OpenBracket = tokens.PopToken<OpenBracketToken>();
            CloseBracket = tokens.PopToken<CloseBracketToken>();
        }
        public override string ToString()
        {
            return BaseType.ToString() + OpenBracket.ToString() + CloseBracket.ToString();
        }
    }
}
