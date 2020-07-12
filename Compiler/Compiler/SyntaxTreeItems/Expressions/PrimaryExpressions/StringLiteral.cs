using Compiler.SyntaxTreeItems.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class StringLiteral : PrimaryExpression
    {
        public readonly Token OpenQuote;
        public readonly Token Text;
        public readonly Token CloseQuote;
        public StringLiteral(TokenCollection tokens, Token openQuoteToken)
        {
            OpenQuote = openQuoteToken;
            Text = tokens.PopToken(TokenType.StringLiteral);
            CloseQuote = tokens.PopToken(TokenType.DoubleQuote);
        }
        public override string ToString()
        {
            return OpenQuote.ToString() + Text.ToString() + CloseQuote.ToString();
        }
    }
}
