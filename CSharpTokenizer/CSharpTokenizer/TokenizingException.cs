using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpTokenizer
{
    class TokenizingException : InvalidOperationException
    {
        public readonly int CharIndex;
        public TokenizingException(int charIndex)
        {
            CharIndex = charIndex;
        }
    }
}
