using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenizer
{
    class UnexpectedEndOfFrameException : InvalidOperationException
    {
    }
    class MissingLineBreakException : InvalidOperationException
    {
        public MissingLineBreakException(IToken token)
        {

        }
    }
    class MissingWhitespaceException : InvalidOperationException
    {
        public MissingWhitespaceException(IToken token)
        {

        }
    }
    class InvalidTokenException : InvalidOperationException
    {
        public InvalidTokenException(IToken token)
        {

        }
    }
    class InvalidEndOfStatementException : InvalidOperationException
    {
        public InvalidEndOfStatementException(IToken token)
        {

        }
    }
}
