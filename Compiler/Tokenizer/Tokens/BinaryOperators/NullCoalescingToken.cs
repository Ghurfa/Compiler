using System;
using System.Collections.Generic;
using System.Text;

namespace Tokenizer
{
    public struct NullCoalescingToken : IToken
    {
        public string Text { get; private set; }
        public int Index { get; private set; }

        public NullCoalescingToken(string text, int index)
        {
            Text = text;
            Index = index;
        }

        public override string ToString() => Text;
    }
}
