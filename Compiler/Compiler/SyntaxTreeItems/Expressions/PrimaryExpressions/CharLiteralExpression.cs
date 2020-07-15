using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class CharLiteralExpression : PrimaryExpression
    {
        public readonly SingleQuoteToken OpenQuote;
        public readonly CharLiteralToken Text;
        public readonly SingleQuoteToken CloseQuote;

        public override IToken LeftToken => OpenQuote;
        public override IToken RightToken => CloseQuote;

        public CharLiteralExpression(TokenCollection tokens, SingleQuoteToken? openQuote = null, CharLiteralToken? text = null, SingleQuoteToken? closeQuote = null)
        {
            OpenQuote = openQuote == null ? tokens.PopToken<SingleQuoteToken>() : (SingleQuoteToken)openQuote;
            Text = text == null ? tokens.PopToken<CharLiteralToken>() : (CharLiteralToken)text;
            CloseQuote = closeQuote == null ? tokens.PopToken<SingleQuoteToken>() : (SingleQuoteToken)closeQuote;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
