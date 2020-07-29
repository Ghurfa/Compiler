using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenizer
{
    public struct ColonToken : IToken
    {
        public string Text { get; private set; }
        public int Index { get; private set; }

        public ColonToken(string text, int index)
        {
            Text = text;
            Index = index;
        }

        public override string ToString() => Text;
    }
}
