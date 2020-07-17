using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;

namespace Compiler.SyntaxTreeItems
{
    public class ArrayType : Type
    {
        public Type BaseType { get; private set; }
        public OpenBracketToken OpenBracket { get; private set; }
        public CloseBracketToken CloseBracket { get; private set; }

        public ArrayType(TokenCollection tokens, Type baseType)
        {
            BaseType = baseType;
            OpenBracket = tokens.PopToken<OpenBracketToken>();
            CloseBracket = tokens.PopToken<CloseBracketToken>();
        }

        public override string ToString()
        {
            string ret = "";
            ret += BaseType.ToString();
            ret += " ";
            ret += OpenBracket.ToString();
            ret += CloseBracket.ToString();
            return ret;
        }
    }
}
