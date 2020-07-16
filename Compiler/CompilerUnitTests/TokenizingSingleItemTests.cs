using Compiler;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using Xunit;

namespace CompilerUnitTests
{
    public class TokenizingSingleItemTests
    {
        [Theory]
        [InlineData(" ", TokenType.Whitespace)]
        [InlineData(" \t", TokenType.Whitespace)]
        [InlineData("//a", TokenType.SingleLineComment)]
        [InlineData("/*a*/", TokenType.MultiLineComment)]
        [InlineData("/*\na */", TokenType.MultiLineComment)]

        [InlineData("public", TokenType.Modifier)]
        [InlineData("private", TokenType.Modifier)]
        [InlineData("static", TokenType.Modifier)]

        [InlineData("namespace", TokenType.NamespaceKeyword)]
        [InlineData("class", TokenType.ClassKeyword)]
        [InlineData("ctor", TokenType.ConstructorKeyword)]
        [InlineData("if", TokenType.IfKeyword)]
        [InlineData("else", TokenType.ElseKeyword)]
        [InlineData("while", TokenType.WhileKeyword)]
        [InlineData("for", TokenType.ForKeyword)]
        [InlineData("foreach", TokenType.ForeachKeyword)]
        [InlineData("switch", TokenType.SwitchKeyword)]
        [InlineData("return", TokenType.ReturnKeyword)]
        [InlineData("break", TokenType.BreakKeyword)]
        [InlineData("continue", TokenType.ContinueKeyword)]
        [InlineData("throw", TokenType.ThrowKeyword)]
        [InlineData("as", TokenType.AsKeyword)]
        [InlineData("new", TokenType.NewKeyword)]

        [InlineData("true", TokenType.TrueKeyword)]
        [InlineData("false", TokenType.FalseKeyword)]

        [InlineData("this", TokenType.ValueKeyword)]
        [InlineData("base", TokenType.ValueKeyword)]
        [InlineData("value", TokenType.ValueKeyword)]

        [InlineData(".", TokenType.Dot)]
        [InlineData(":", TokenType.Colon)]
        [InlineData(";", TokenType.Semicolon)]
        [InlineData("\\", TokenType.Backslash)]
        [InlineData("?", TokenType.QuestionMark)]
        [InlineData(",", TokenType.Comma)]
        [InlineData("{", TokenType.OpenCurly)]
        [InlineData("}", TokenType.CloseCurly)]
        [InlineData("(", TokenType.OpenPeren)]
        [InlineData(")", TokenType.ClosePeren)]
        [InlineData("[", TokenType.OpenBracket)]
        [InlineData("]", TokenType.CloseBracket)]
        [InlineData("?.", TokenType.NullCondDot)]
        [InlineData("?[", TokenType.NullCondOpenBracket)]

        [InlineData("++", TokenType.Increment)]
        [InlineData("--", TokenType.Decrement)]
        [InlineData("!", TokenType.Not)]
        [InlineData("~", TokenType.BitwiseNot)]

        [InlineData("+", TokenType.Plus)]
        [InlineData("-", TokenType.Minus)]
        [InlineData("*", TokenType.Asterisk)]
        [InlineData("/", TokenType.Divide)]
        [InlineData("%", TokenType.Modulo)]
        [InlineData("&", TokenType.BitwiseAnd)]
        [InlineData("|", TokenType.BitwiseOr)]
        [InlineData("^", TokenType.BitwiseXor)]
        [InlineData("<<", TokenType.LeftShift)]
        [InlineData(">>", TokenType.RightShift)]
        [InlineData("??", TokenType.NullCoalescing)]

        [InlineData("=", TokenType.Assign)]
        [InlineData(":=", TokenType.DeclAssign)]
        [InlineData("+=", TokenType.PlusAssign)]
        [InlineData("-=", TokenType.MinusAssign)]
        [InlineData("*=", TokenType.MultiplyAssign)]
        [InlineData("/=", TokenType.DivideAssign)]
        [InlineData("%=", TokenType.ModuloAssign)]
        [InlineData("&=", TokenType.BitwiseAndAssign)]
        [InlineData("|=", TokenType.BitwiseOrAssign)]
        [InlineData("^=", TokenType.BitwiseXorAssign)]
        [InlineData("<<=", TokenType.LeftShiftAssign)]
        [InlineData(">>=", TokenType.RightShiftAssign)]
        [InlineData("??=", TokenType.NullCoalescingAssign)]

        [InlineData("==", TokenType.Equals)]
        [InlineData("!=", TokenType.NotEquals)]
        [InlineData("&&", TokenType.And)]
        [InlineData("||", TokenType.Or)]

        [InlineData("<", TokenType.LessThan)]
        [InlineData(">", TokenType.GreaterThan)]
        [InlineData("<=", TokenType.LessThanOrEqualTo)]
        [InlineData(">=", TokenType.GreaterThanOrEqualTo)]
        public void ParseSingleTokenTest(string text, TokenType expectedType)
        {
            var tokens = Tokenizer.Tokenize(text);
            int count = 0;
            foreach (IToken token in tokens)
            {
                count++;
                Assert.Equal(expectedType, token.Type);
                Assert.Equal(text, token.Text);
            }
            Assert.Equal(1, count);
        }

        string newLine = Environment.NewLine;
        [Fact]
        public void ParseWhitespaceWithLineBreakTest()
        {
            void test(string text, TokenType expectedType)
            {
                var tokens = Tokenizer.Tokenize(text);
                int count = 0;
                foreach (IToken token in tokens)
                {
                    Assert.Equal(expectedType, token.Type);
                    Assert.Equal(text, token.Text);
                    count++;
                }
                Assert.Equal(1, count);
            }
            test(newLine, TokenType.WhitespaceWithLineBreak);
            test(newLine + newLine, TokenType.WhitespaceWithLineBreak);
            test(" " + newLine, TokenType.WhitespaceWithLineBreak);
            test(newLine + " \t", TokenType.WhitespaceWithLineBreak);
        }

        [Fact]
        public void ParseSingleLineCommentWithLineBreaTestk()
        {
            var tokens = Tokenizer.Tokenize("//a\r\n");
            int count = 0;
            foreach (IToken token in tokens)
            {
                if (count == 0)
                {
                    Assert.Equal(TokenType.SingleLineComment, token.Type);
                    Assert.Equal("//a", token.Text);
                }
                else
                {
                    Assert.Equal(TokenType.WhitespaceWithLineBreak, token.Type);
                    Assert.Equal("\r\n", token.Text);
                }
                count++;
            }
            Assert.Equal(2, count);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("string")]
        [InlineData("+")]
        [InlineData("a += b;")]
        [InlineData("{non-interpolation}")]
        [InlineData("this is a string literal")]
        [InlineData("a:int := 12")]
        [InlineData("multi\nline\nstring")]
        public void ParseStringLiteralTest(string innerText)
        {
            var tokens = Tokenizer.Tokenize('"' + innerText + '"');

            int count = 0;
            foreach (IToken token in tokens)
            {
                if (count == 0 || count == 2)
                {
                    Assert.Equal(TokenType.DoubleQuote, token.Type);
                    Assert.Equal("\"", token.Text);
                }
                else
                {
                    Assert.Equal(TokenType.StringLiteral, token.Type);
                    Assert.Equal(innerText, token.Text);
                }
                count++;
            }
            Assert.Equal(3, count);
        }
        
        [Theory]
        [InlineData("\\\\", "\\")]
        [InlineData("\\\"", "\"")]
        [InlineData("\\u1ab0", "\u1ab0")]
        [InlineData("\\x1a0", "\x1a0")]
        public void ParseEscapedStringLiteralTest(string innerText, string expectedText)
        {
            void test(string text, string expected)
            {
                var tokens = Tokenizer.Tokenize('"' + text + '"');

                int count = 0;
                foreach (IToken token in tokens)
                {
                    if (count == 0 || count == 2)
                    {
                        Assert.Equal(TokenType.DoubleQuote, token.Type);
                        Assert.Equal("\"", token.Text);
                    }
                    else
                    {
                        Assert.Equal(TokenType.StringLiteral, token.Type);
                        Assert.Equal(expected, token.Text);
                    }
                    count++;
                }
                Assert.Equal(3, count);
            }
            test(innerText, expectedText);
            test("Z" + innerText, "Z" + expectedText);
            test(innerText + "Z", expectedText + "Z");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("\t")]
        [InlineData("+")]
        [InlineData("\x1a0")]
        [InlineData("\\\\", "\\")]
        [InlineData("\\\"", "\"")]
        [InlineData("\\u1ab0", "\u1ab0")]
        [InlineData("\\x1a0", "\x1a0")]
        public void ParseCharLiteralTest(string innerText, string expectedText = null)
        {
            if (expectedText == null) expectedText = innerText;

            var tokens = Tokenizer.Tokenize('\'' + innerText + '\'');

            int count = 0;
            foreach (IToken token in tokens)
            {
                if (count == 0 || count == 2)
                {
                    Assert.Equal(TokenType.SingleQuote, token.Type);
                    Assert.Equal("'", token.Text);
                }
                else
                {
                    Assert.Equal(TokenType.CharLiteral, token.Type);
                    Assert.Equal(expectedText, token.Text);
                }
                count++;
            }
            Assert.Equal(3, count);
        }

        [Theory]
        [InlineData("")]
        [InlineData("ab")]
        public void ParseInvalidCharLiteralTest(string innerText)
        {
            Assert.Throws<TokenizingException>(() => Tokenizer.Tokenize('\'' + innerText + '\''));
        }
    }
}
