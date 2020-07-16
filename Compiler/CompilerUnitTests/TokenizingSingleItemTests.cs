using Compiler;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using Xunit;

namespace CompilerUnitTests
{
    public class TokenizingSingleItemTests
    {
        [Theory]
        [InlineData(" ", typeof(WhitespaceToken))]
        [InlineData(" \t", typeof(WhitespaceToken))]
        [InlineData("//a", typeof(SingleLineCommentToken))]
        [InlineData("/*a*/", typeof(MultiLineCommentToken))]
        [InlineData("/*\na */", typeof(MultiLineCommentToken))]

        [InlineData("public", typeof(ModifierToken))]
        [InlineData("private", typeof(ModifierToken))]
        [InlineData("static", typeof(ModifierToken))]

        [InlineData("namespace", typeof(NamespaceKeywordToken))]
        [InlineData("class", typeof(ClassKeywordToken))]
        [InlineData("ctor", typeof(ConstructorKeywordToken))]
        [InlineData("if", typeof(IfKeywordToken))]
        [InlineData("else", typeof(ElseKeywordToken))]
        [InlineData("while", typeof(WhileKeywordToken))]
        [InlineData("for", typeof(ForKeywordToken))]
        [InlineData("foreach", typeof(ForeachKeywordToken))]
        [InlineData("switch", typeof(SwitchKeywordToken))]
        [InlineData("return", typeof(ReturnKeywordToken))]
        [InlineData("break", typeof(BreakKeywordToken))]
        [InlineData("continue", typeof(ContinueKeywordToken))]
        [InlineData("throw", typeof(ThrowKeywordToken))]
        [InlineData("as", typeof(AsKeywordToken))]
        [InlineData("new", typeof(NewKeywordToken))]

        [InlineData("true", typeof(TrueKeywordToken))]
        [InlineData("false", typeof(FalseKeywordToken))]

        [InlineData("this", typeof(ValueKeywordToken))]
        [InlineData("base", typeof(ValueKeywordToken))]
        [InlineData("value", typeof(ValueKeywordToken))]

        [InlineData(".", typeof(DotToken))]
        [InlineData(":", typeof(ColonToken))]
        [InlineData(";", typeof(SemicolonToken))]
        [InlineData("\\", typeof(BackslashToken))]
        [InlineData("?", typeof(QuestionMarkToken))]
        [InlineData(",", typeof(CommaToken))]
        [InlineData("{", typeof(OpenCurlyToken))]
        [InlineData("}", typeof(CloseCurlyToken))]
        [InlineData("(", typeof(OpenPerenToken))]
        [InlineData(")", typeof(ClosePerenToken))]
        [InlineData("[", typeof(OpenBracketToken))]
        [InlineData("]", typeof(CloseBracketToken))]
        [InlineData("?.", typeof(NullCondDotToken))]
        [InlineData("?[", typeof(NullCondOpenBracketToken))]

        [InlineData("++", typeof(IncrementToken))]
        [InlineData("--", typeof(DecrementToken))]
        [InlineData("!", typeof(NotToken))]
        [InlineData("~", typeof(BitwiseNotToken))]

        [InlineData("+", typeof(PlusToken))]
        [InlineData("-", typeof(MinusToken))]
        [InlineData("*", typeof(AsteriskToken))]
        [InlineData("/", typeof(DivideToken))]
        [InlineData("%", typeof(ModuloToken))]
        [InlineData("&", typeof(BitwiseAndToken))]
        [InlineData("|", typeof(BitwiseOrToken))]
        [InlineData("^", typeof(BitwiseXorToken))]
        [InlineData("<<", typeof(LeftShiftToken))]
        [InlineData(">>", typeof(RightShiftToken))]
        [InlineData("??", typeof(NullCoalescingToken))]

        [InlineData("=", typeof(AssignToken))]
        [InlineData(":=", typeof(DeclAssignToken))]
        [InlineData("+=", typeof(PlusAssignToken))]
        [InlineData("-=", typeof(MinusAssignToken))]
        [InlineData("*=", typeof(MultiplyAssignToken))]
        [InlineData("/=", typeof(DivideAssignToken))]
        [InlineData("%=", typeof(ModuloAssignToken))]
        [InlineData("&=", typeof(BitwiseAndAssignToken))]
        [InlineData("|=", typeof(BitwiseOrAssignToken))]
        [InlineData("^=", typeof(BitwiseXorAssignToken))]
        [InlineData("<<=", typeof(LeftShiftAssignToken))]
        [InlineData(">>=", typeof(RightShiftAssignToken))]
        [InlineData("??=", typeof(NullCoalescingAssignToken))]

        [InlineData("==", typeof(EqualsToken))]
        [InlineData("!=", typeof(NotEqualsToken))]
        [InlineData("&&", typeof(AndToken))]
        [InlineData("||", typeof(OrToken))]

        [InlineData("<", typeof(LessThanToken))]
        [InlineData(">", typeof(GreaterThanToken))]
        [InlineData("<=", typeof(LessThanOrEqualToToken))]
        [InlineData(">=", typeof(GreaterThanOrEqualToToken))]
        public void ParseSingleTokenTest(string text, Type expectedType)
        {
            var tokens = Tokenizer.Tokenize(text);
            int count = 0;
            foreach (IToken token in tokens)
            {
                count++;
                Assert.Equal(expectedType, token.GetType());
                Assert.Equal(text, token.Text);
            }
            Assert.Equal(1, count);
        }

        string newLine = Environment.NewLine;
        [Fact]
        public void ParseWhitespaceWithLineBreakTest()
        {
            void test(string text)
            {
                var tokens = Tokenizer.Tokenize(text);
                int count = 0;
                foreach (IToken token in tokens)
                {
                    Assert.IsType<WhitespaceWithLineBreakToken>(token);
                    Assert.Equal(text, token.Text);
                    count++;
                }
                Assert.Equal(1, count);
            }
            test(newLine);
            test(newLine + newLine);
            test(" " + newLine);
            test(newLine + " \t");
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
                    Assert.IsType<SingleLineCommentToken>(token);
                    Assert.Equal("//a", token.Text);
                }
                else
                {
                    Assert.IsType<WhitespaceWithLineBreakToken>(token);
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
        [InlineData("'")]
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
                    Assert.IsType<DoubleQuoteToken>(token);
                    Assert.Equal("\"", token.Text);
                }
                else
                {
                    Assert.IsType<StringLiteralToken>(token);
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
                        Assert.IsType<DoubleQuoteToken>(token);
                        Assert.Equal("\"", token.Text);
                    }
                    else
                    {
                        Assert.IsType<StringLiteralToken>(token);
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
        [InlineData("\"")]
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
                    Assert.IsType<SingleQuoteToken>(token);
                    Assert.Equal("'", token.Text);
                }
                else
                {
                    Assert.IsType<CharLiteralToken>(token);
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
