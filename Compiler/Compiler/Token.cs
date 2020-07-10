using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Compiler
{
    public enum TokenType
    {
        Modifier,
        ControlKeyword,
        PrimitiveType,
        BlockMarker,
        NewKeyword,
        ValueKeyword,
        Identifier,
        Whitespace,
        SyntaxChar,
        Operator,
        IntLiteral,
        StringLiteral,
        CharLiteral,
    }
    [DebuggerDisplay("{Type} \t {Text}")]
    public struct Token
    {
        public string Text;
        public TokenType Type;
        public Token(string text, TokenType type)
        {
            Text = text;
            Type = type;
        }
    }
}
