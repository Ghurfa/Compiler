using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public struct NotEqualsToken : IToken
    {
        public string Text { get; private set; }
        public int Index { get; private set; }

        public NotEqualsToken(string text, int index)
        {
            Text = text;
            Index = index;
        }

        public override string ToString() => Text;
    }
}
