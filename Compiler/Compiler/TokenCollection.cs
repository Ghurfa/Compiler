using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class TokenCollection : IEnumerable<Token>
    {
        private Token[] tokens;
        int pointer = 0;

        public TokenCollection(Token[] tokens)
        {
            this.tokens = tokens;
        }
        public TokenCollection(LinkedList<Token> tokens)
        {
            this.tokens = tokens.ToArray();
        }
        public Token PeekToken()
        {
            if (pointer > tokens.Length) throw new SyntaxTreeBuildingException();
            while (tokens[pointer].IsTrivia)
            {
                pointer++;
                if (pointer > tokens.Length) throw new SyntaxTreeBuildingException();
            }
            return tokens[pointer];
        }
        public bool PopIfMatches(out Token tokenIfMatches, TokenType type)
        {
            Token peek = PeekToken();
            if (peek.Type == type)
            {
                pointer++;
                tokenIfMatches = peek;
                return true;
            }
            tokenIfMatches = default;
            return false;
        }
        public bool PopIfMatches(out Token tokenIfMatches, TokenType type, string[] possibleText)
        {
            Token peek = PeekToken();
            if (peek.Type == type && possibleText.Contains(peek.Text))
            {
                pointer++;
                tokenIfMatches = peek;
                return true;
            }
            tokenIfMatches = default;
            return false;
        }

        public Token PopToken()
        {
            Token ret = PeekToken();
            pointer++;
            return ret;
        }
        public Token PopToken(TokenType expectedType)
        {
            Token ret = PeekToken();
            if (ret.Type != expectedType) throw new SyntaxTreeBuildingException(ret);
            pointer++;
            return ret;
        }

        private string[] modifiers = new string[] { "public", "private", "static" };
        public Token[] ReadModifiers()
        {
            return ReadModifiers(modifiers);
        }
        public Token[] ReadModifiers(string[] validModifiers)
        {
            LinkedList<Token> modifiers = new LinkedList<Token>();
            Token token;
            while (PopIfMatches(out token, TokenType.Modifier, validModifiers))
            {
                modifiers.AddLast(token);
            }
            return modifiers.ToArray();
        }

        public IEnumerator<Token> GetEnumerator()
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                yield return tokens[i];
            }
        }
        public IEnumerator<Token> NonWhitespaceTokens()
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                if (!tokens[i].IsTrivia) yield return tokens[i];
            }
        }
        public IEnumerable<Token> GetContext(Token token, int tokensBefore, int tokensAfter)
        {
            LinkedList<Token> beforeTokens = new LinkedList<Token>();

            int ptr = token.Index - 1;
            int beforeCount = 0;
            while(beforeCount < tokensBefore && ptr > 0)
            { 
                if(!tokens[ptr].IsTrivia)
                {
                    beforeTokens.AddFirst(tokens[ptr]);
                    beforeCount++;
                }
                ptr--;
            }

            foreach (Token beforeToken in beforeTokens)
                yield return beforeToken;

            yield return token;

            ptr = token.Index + 1;
            int afterCount = 0;
            while(afterCount < tokensAfter && ptr < tokens.Length)
            {
                if(!tokens[ptr].IsTrivia)
                {
                    yield return tokens[ptr];
                    afterCount++;
                }
                ptr++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
