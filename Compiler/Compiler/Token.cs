using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Compiler
{
    public enum TokenType
    {
        NamespaceKeyword,
        ClassKeyword,
        ConstructorKeyword,

        Modifier,

        IfKeyword,
        ForKeyword,
        ForeachKeyword,
        WhileKeyword,
        ReturnKeyword,
        BreakKeyword,
        ContinueKeyword,

        AsKeyword,

        PrimitiveType,
        NewKeyword,
        ValueKeyword,
        Identifier,

        Whitespace,
        SingleLineComment,
        MultiLineComment,

        Dot,
        Comma,
        OpenPeren,
        ClosePeren,
        OpenBracket,
        CloseBracket,
        OpenCurly,
        CloseCurly,
        QuestionMark,
        Colon,
        Semicolon,
        SingleQuote,
        DoubleQuote,

        Assign,
        DeclAssign,
        Plus,
        Minus,
        Asterisk,
        Divide,
        Modulo,
        PlusAssign,
        MinusAssign,
        TimesAssign,
        DivideAssign,
        ModuloAssign,
        Increment,
        Decrement,
        Equals,
        NotEquals,
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
        And,
        Or,
        Not,
        BitwiseAnd,
        BitwiseOr,
        BitwiseXor,
        BitwiseNot,
        LeftShift,
        RightShift,

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
