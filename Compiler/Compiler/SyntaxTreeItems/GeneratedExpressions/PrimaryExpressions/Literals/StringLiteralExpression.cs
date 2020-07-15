using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class StringLiteralExpression : PrimaryExpression
    {
        public readonly DoubleQuoteToken OpenQuote;
        public readonly StringLiteralToken Text;
        public readonly DoubleQuoteToken CloseQuote;

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
