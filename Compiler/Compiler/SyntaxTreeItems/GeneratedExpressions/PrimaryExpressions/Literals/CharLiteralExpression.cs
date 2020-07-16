using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class CharLiteralExpression : PrimaryExpression
    {
        public SingleQuoteToken OpenQuote { get; private set; }
        public CharLiteralToken Text { get; private set; }
        public SingleQuoteToken CloseQuote { get; private set; }

        public CharLiteralExpression(TokenCollection tokens, SingleQuoteToken? openQuote = null, CharLiteralToken? text = null, SingleQuoteToken? closeQuote = null)
        {
            OpenQuote = openQuote == null ? tokens.PopToken<SingleQuoteToken>() : (SingleQuoteToken)openQuote;
            Text = text == null ? tokens.PopToken<CharLiteralToken>() : (CharLiteralToken)text;
            CloseQuote = closeQuote == null ? tokens.PopToken<SingleQuoteToken>() : (SingleQuoteToken)closeQuote;
        }

        public override string ToString()
        {
            string ret = "";
            ret += OpenQuote.ToString();
            ret += " ";
            ret += Text.ToString();
            ret += " ";
            ret += CloseQuote.ToString();
            return ret;
        }
    }
}
