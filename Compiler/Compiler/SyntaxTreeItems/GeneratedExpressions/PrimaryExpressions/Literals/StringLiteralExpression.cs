using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class StringLiteralExpression : PrimaryExpression
    {
        public DoubleQuoteToken OpenQuote { get; private set; }
        public StringLiteralToken Text { get; private set; }
        public DoubleQuoteToken CloseQuote { get; private set; }

        public StringLiteralExpression(TokenCollection tokens, DoubleQuoteToken? openQuote = null, StringLiteralToken? text = null, DoubleQuoteToken? closeQuote = null)
        {
            OpenQuote = openQuote == null ? tokens.PopToken<DoubleQuoteToken>() : (DoubleQuoteToken)openQuote;
            Text = text == null ? tokens.PopToken<StringLiteralToken>() : (StringLiteralToken)text;
            CloseQuote = closeQuote == null ? tokens.PopToken<DoubleQuoteToken>() : (DoubleQuoteToken)closeQuote;
        }

        public override string ToString()
        {
            string ret = "";
            ret += OpenQuote.ToString();
            ret += Text.ToString();
            ret += CloseQuote.ToString();
            return ret;
        }
    }
}
