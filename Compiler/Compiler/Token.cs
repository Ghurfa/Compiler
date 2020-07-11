using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Compiler
{
    public enum TokenType
    {
        //Block marker keywords
        NamespaceKeyword,
        ClassKeyword,
        ConstructorKeyword,

        //Todo: break down into more specific types
        Modifier,
        PrimitiveType,
        ValueKeyword,
        Identifier,

        //Control keywords
        IfKeyword,
        ElseKeyword,
        ForKeyword,
        ForeachKeyword,
        WhileKeyword,
        ReturnKeyword,
        BreakKeyword,
        ContinueKeyword,

        //Miscellaneous keywords
        AsKeyword,
        NewKeyword,

        //Trivia
        Whitespace,
        SingleLineComment,
        MultiLineComment,

        //Syntax characters
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

        //Unary operators
        Not,
        BitwiseNot,
        Increment,
        Decrement,

        //Binary operators
        Plus,
        Minus,
        Asterisk,
        Divide,
        Modulo,
        BitwiseAnd,
        BitwiseOr,
        BitwiseXor,
        LeftShift,
        RightShift,

        //Binary assign operators
        Assign,
        DeclAssign,
        PlusAssign,
        MinusAssign,
        TimesAssign,
        DivideAssign,
        ModuloAssign,
        BitwiseAndAssign,
        BitwiseOrAssign,
        BitwiseXorAssign,
        LeftShiftAssign,
        RightShiftAssign,

        //Binary boolean operators
        Equals,
        NotEquals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqualTo,
        LessThanOrEqualTo,
        And,
        Or,

        //Literals
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
