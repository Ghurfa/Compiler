using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Compiler
{
    public interface IToken
    {
        string Text { get; }
        int Index { get; }
    }
}
