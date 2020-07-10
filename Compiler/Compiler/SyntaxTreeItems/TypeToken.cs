using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class TypeToken
    {
        public readonly Token BaseType;
        public readonly Token? OpenArrayBracket;
        public readonly Token? CloseArrayBracket;
        public TypeToken(TokenCollection tokens)
        {
            BaseType = tokens.PopToken();
            if (BaseType.Type != TokenType.PrimitiveType && 
                BaseType.Type != TokenType.Identifier) throw new SyntaxTreeBuildingException(BaseType);

            if(tokens.PopIfMatches(out Token openArr, TokenType.SyntaxChar, "["))
            {
                OpenArrayBracket = openArr;
                CloseArrayBracket = tokens.PopToken(TokenType.SyntaxChar, "]");
            }
        }
    }
}
