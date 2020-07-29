using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenizer
{
    public class TokenizingException : InvalidOperationException
    {
        public readonly int CharIndex;
        public TokenizingException(int charIndex)
        {
            CharIndex = charIndex;
        }
    }
}
