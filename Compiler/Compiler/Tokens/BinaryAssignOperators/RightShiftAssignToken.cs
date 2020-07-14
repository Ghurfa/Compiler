using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public struct RightShiftAssignToken : IToken
    {
        public string Text { get; private set; }
        public int Index { get; private set; }

        public RightShiftAssignToken(string text, int index)
        {
            Text = text;
            Index = index;
        }

        public override string ToString() => Text;
    }
}
