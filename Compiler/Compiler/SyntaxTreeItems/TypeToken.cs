﻿using System;
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
        private static string[] typeKeywords = { "int", "string", "bool", "byte", "float", "double" };
        public TypeToken(LinkedList<Token> tokens)
        {
            BaseType = tokens.GetToken();
            if (BaseType.Type == TokenType.Keyword)
            {
                if (!typeKeywords.Contains(BaseType.Text)) throw new SyntaxTreeBuildingException(BaseType);
            }
            else if (BaseType.Type != TokenType.Identifier) throw new SyntaxTreeBuildingException(BaseType);

            if(tokens.PopIfMatches(out Token openArr, TokenType.SyntaxChar, "["))
            {
                OpenArrayBracket = openArr;
                CloseArrayBracket = tokens.GetToken(TokenType.SyntaxChar, "]");
            }
        }
    }
}