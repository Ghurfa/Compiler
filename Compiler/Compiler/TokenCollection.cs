using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class TokenCollection : IEnumerable<IToken>
    {
        private IToken[] tokens;
        int pointer = 0;
        int lastUsedToken = 0;

        public TokenCollection(IToken[] tokens)
        {
            this.tokens = tokens;
        }
        public TokenCollection(LinkedList<IToken> tokens)
        {
            this.tokens = tokens.ToArray();
        }
        public IToken PeekToken()
        {
            if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            while (tokens[pointer] is ITriviaToken)
            {
                pointer++;
                if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            }
            return tokens[pointer];
        }
        public bool PopIfMatches<T>(out T tokenIfMatches)
        {
            IToken peek = PeekToken();
            if (peek is T matched)
            {
                lastUsedToken = pointer;
                pointer++;
                tokenIfMatches = matched;
                return true;
            }
            tokenIfMatches = default;
            return false;
        }

        /*public IToken PopToken()
        {
            IToken ret = PeekToken();
            lastUsedToken = pointer;
            pointer++;
            return ret;
        }*/
        public T PopToken<T>()
        {
            IToken peek = PeekToken();
            if(peek is T ret)
            {
                lastUsedToken = pointer;
                pointer++;
                return ret;
            }
            else throw new InvalidTokenException(peek);
        }

        public IToken EnsureValidStatementEnding()
        {
            pointer = lastUsedToken + 1;
            if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            while (tokens[pointer] is ITriviaToken && !(tokens[pointer] is WhitespaceWithLineBreakToken))
            {
                pointer++;
                if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            }

            IToken endingToken = tokens[pointer];
            switch (endingToken)
            {
                case SemicolonToken semicolon:
                    pointer++;
                    return semicolon;
                case WhitespaceWithLineBreakToken _:
                    pointer++;
                    return null;
                case CloseBracketToken _:
                case CloseCurlyToken _:
                case ClosePerenToken _:
                    return null;
                default:
                    throw new InvalidEndOfStatementException(endingToken);
            }
        }

        public void EnsureWhitespaceAfter(IToken token)
        {
            int save = pointer;
            pointer = token.Index + 1;
            if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            var tokenAfter = tokens[pointer];
            pointer = save;
            if (!(tokenAfter is IWhitespaceToken))
            {
                throw new MissingWhitespaceException(tokens[pointer]);
            }
        }

        public void EnsureLineBreakAfter(IToken token)
        {
            int save = pointer;
            pointer = token.Index + 1;
            if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            while (tokens[pointer] is ITriviaToken && !(tokens[pointer] is WhitespaceWithLineBreakToken))
            {
                pointer++;
                if (pointer > tokens.Length) throw new UnexpectedEndOfFrameException();
            }

            IToken endingToken = tokens[pointer];
            pointer = save;
            if (!(endingToken is WhitespaceWithLineBreakToken)) throw new MissingLineBreakException(endingToken);
        }

        public IEnumerator<IToken> GetEnumerator()
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                yield return tokens[i];
            }
        }
        public IEnumerator<IToken> NonWhitespaceTokens()
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                if (!(tokens[i] is ITriviaToken)) yield return tokens[i];
            }
        }
        public IEnumerable<IToken> GetContext(IToken token, int tokensBefore, int tokensAfter)
        {
            LinkedList<IToken> beforeTokens = new LinkedList<IToken>();

            int ptr = token.Index - 1;
            int beforeCount = 0;
            while(beforeCount < tokensBefore && ptr > 0)
            { 
                if(!(tokens[ptr] is ITriviaToken))
                {
                    beforeTokens.AddFirst(tokens[ptr]);
                    beforeCount++;
                }
                ptr--;
            }

            foreach (IToken beforeToken in beforeTokens)
                yield return beforeToken;

            yield return token;

            ptr = token.Index + 1;
            int afterCount = 0;
            while(afterCount < tokensAfter && ptr < tokens.Length)
            {
                if(!(tokens[ptr] is ITriviaToken))
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
