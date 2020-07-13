using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class TokenCollection : IEnumerable<Token>
    {
        private Token[] tokens;
        int pointer = 0;
        int lastUsedToken = 0;

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
            if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            while (tokens[pointer].IsTrivia)
            {
                pointer++;
                if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            }
            return tokens[pointer];
        }
        public bool PopIfMatches(out Token tokenIfMatches, TokenType type)
        {
            Token peek = PeekToken();
            if (peek.Type == type)
            {
                lastUsedToken = pointer;
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
            lastUsedToken = pointer;
            pointer++;
            return ret;
        }
        public Token PopToken(TokenType expectedType)
        {
            Token ret = PeekToken();
            if (ret.Type != expectedType) throw new InvalidTokenException(ret);
            lastUsedToken = pointer;
            pointer++;
            return ret;
        }

        public Token? EnsureValidStatementEnding()
        {
            pointer = lastUsedToken + 1;
            if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            while (tokens[pointer].IsTrivia && tokens[pointer].Type != TokenType.WhitespaceWithLineBreak)
            {
                pointer++;
                if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            }

            Token endingToken = tokens[pointer];
            switch (endingToken.Type)
            {
                case TokenType.Semicolon:
                    pointer++;
                    return endingToken;
                case TokenType.WhitespaceWithLineBreak:
                    pointer++;
                    return null;
                case TokenType.CloseBracket:
                case TokenType.CloseCurly:
                case TokenType.ClosePeren:
                    return null;
                default:
                    throw new InvalidEndOfStatementException(endingToken);
            }
        }

        public void EnsureWhitespace()
        {
            pointer = lastUsedToken + 1;
            if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            TokenType type = tokens[pointer].Type;
            if (type != TokenType.Whitespace && type != TokenType.WhitespaceWithLineBreak)
            {
                throw new MissingWhitespaceException(tokens[pointer]);
            }
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
