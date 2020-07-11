using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Compiler
{
    public enum TokenType
    {
        Identifier,

        //Block marker keywords
        NamespaceKeyword,
        ClassKeyword,
        ConstructorKeyword,

        //Todo: break down into more specific types
        Modifier,
        PrimitiveType,
        ValueKeyword,

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
        public int Index;
        public Token(string text, TokenType type, int index)
        {
            Text = text;
            Type = type;
            Index = index;
        }
        public bool IsTrivia => Type == TokenType.Whitespace || Type == TokenType.SingleLineComment || Type == TokenType.MultiLineComment;
    }
}
