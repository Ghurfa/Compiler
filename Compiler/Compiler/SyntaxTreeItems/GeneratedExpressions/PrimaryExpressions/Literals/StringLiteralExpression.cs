using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class StringLiteralExpression : PrimaryExpression
    {
        public DoubleQuoteToken OpenQuote { get; private set; }
        public StringLiteralToken Text { get; private set; }
        public DoubleQuoteToken CloseQuote { get; private set; }

        public StringLiteralExpression(TokenCollection tokens, DoubleQuoteToken openQuote)
        {
            OpenQuote = openQuote;
            Text = tokens.PopToken<StringLiteralToken>();;
            CloseQuote = tokens.PopToken<DoubleQuoteToken>();;
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
