using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    class TokenCollectionBuilder
    {
        LinkedList<Token> tokens;
        int index;
        public TokenCollectionBuilder()
        {
            tokens = new LinkedList<Token>();
            index = 0;
        }

        public void Add(string text, TokenType type)
        {
            tokens.AddLast(new Token(text, type, index));
            index++;
        }

        public TokenCollection GetCollection()
        {
            return new TokenCollection(tokens);
        }
    }
}
