using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class StringLiteralExpression : PrimaryExpression
    {
        public readonly DoubleQuoteToken OpenQuote;
        public readonly StringLiteralToken Text;
        public readonly DoubleQuoteToken CloseQuote;

        public override IToken LeftToken => OpenQuote;
        public override IToken RightToken => CloseQuote;

        public StringLiteralExpression(TokenCollection tokens, DoubleQuoteToken? openQuote = null, StringLiteralToken? text = null, DoubleQuoteToken? closeQuote = null)
        {
            OpenQuote = openQuote == null ? tokens.PopToken<DoubleQuoteToken>() : (DoubleQuoteToken)openQuote;
            Text = text == null ? tokens.PopToken<StringLiteralToken>() : (StringLiteralToken)text;
            CloseQuote = closeQuote == null ? tokens.PopToken<DoubleQuoteToken>() : (DoubleQuoteToken)closeQuote;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
