using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class CharLiteralExpression : PrimaryExpression
    {
        public SingleQuoteToken OpenQuote { get; private set; }
        public CharLiteralToken Text { get; private set; }
        public SingleQuoteToken CloseQuote { get; private set; }

        public CharLiteralExpression(TokenCollection tokens, SingleQuoteToken openQuote)
        {
            OpenQuote = openQuote;
            Text = tokens.PopToken<CharLiteralToken>();;
            CloseQuote = tokens.PopToken<SingleQuoteToken>();;
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
