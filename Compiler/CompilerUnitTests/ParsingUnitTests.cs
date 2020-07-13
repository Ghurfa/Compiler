using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SimpleTokenizer;
using System;
using Xunit;

namespace SimpleTokenizerUnitTests
{
    public class ParsingUnitTests
    {
        [Theory]
        [InlineData("public")]
        [InlineData("static")]
        [InlineData("string")]
        public void ParseKeywordTest(string text)
        {
            var list = SimpleTokenizer.SimpleTokenizer.Tokenize(text);
            var token = Assert.Single(list);
            Assert.Equal(text, token.Text);
            Assert.Equal(TokenType.Keyword, token.Type);
        }

        [Theory]
        [InlineData("Public")]
        [InlineData("a1a")]
        [InlineData("awer")]
        [InlineData("\ud801")]

        public void ParseIdentifierTest(string text)
        {
            var list = SimpleTokenizer.SimpleTokenizer.Tokenize(text);
            var token = Assert.Single(list);
            Assert.Equal(text, token.Text);
            Assert.Equal(TokenType.Identifier, token.Type);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("0134")]
        public void ParseNumberTest(string text)
        {
            var list = SimpleTokenizer.SimpleTokenizer.Tokenize(text);
            var token = Assert.Single(list);
            Assert.Equal(text, token.Text);
            Assert.Equal(TokenType.Literal, token.Type);
        }

        [Theory]
        [InlineData("\"\"")]
        [InlineData("\"asdf   s\"")]
        [InlineData("\"\\\"\"")]
        [InlineData("'\"'")]
        [InlineData("'\n'")]
        public void ParseLiteralTest(string text)
        {
            var list = SimpleTokenizer.SimpleTokenizer.Tokenize(text);
            Assert.Collection(list,
                (token) =>
                {
                    Assert.Equal(TokenType.SyntaxChar, token.Type);
                    Assert.Equal(text.Substring(0, 1), token.Text);
                },
                (token) =>
                {
                    Assert.Equal(TokenType.Literal, token.Type);
                    Assert.Equal(text.Substring(1, text.Length - 2), token.Text);
                },
                (token) =>
                {
                    Assert.Equal(TokenType.SyntaxChar, token.Type);
                    Assert.Equal(text.Substring(text.Length - 1, 1), token.Text);
                });
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("\r")]
        public void ParseWhitespace(string text)
        {
            var list = SimpleTokenizer.SimpleTokenizer.Tokenize(text);
            var token = Assert.Single(list);
            Assert.Equal(text, token.Text);
            Assert.Equal(TokenType.Whitespace, token.Type);
        }

        [Theory]
        [InlineData("}")]
        [InlineData("[")]
        [InlineData(";")]
        public void ParseSyntaxChar(string text)
        {
            var list = SimpleTokenizer.SimpleTokenizer.Tokenize(text);
            var token = Assert.Single(list);
            Assert.Equal(text, token.Text);
            Assert.Equal(TokenType.SyntaxChar, token.Type);
        }

        [Theory]
        [InlineData("=")]
        [InlineData("+")]
        [InlineData("+=")]
        [InlineData("==")]
        [InlineData("<")]
        [InlineData(">=")]
        [InlineData("!")]
        [InlineData("||")]
        [InlineData("&")]
        public void ParseOperatorsTest(string text)
        {
            var list = SimpleTokenizer.SimpleTokenizer.Tokenize(text);
            var token = Assert.Single(list);
            Assert.Equal(text, token.Text);
            Assert.Equal(TokenType.Operator, token.Type);
        }

        [Theory]
        [InlineData("\"")]
        [InlineData("'")]
        [InlineData("\"'")]
        [InlineData("1a")]
        [InlineData("1g0")]
        [InlineData("+-")]
        public void InvalidTokensTest(string text)
        {
            Action action = () => SimpleTokenizer.SimpleTokenizer.Tokenize(text);
            Assert.Throws<TokenizingException>(action);
        }
    }
}
