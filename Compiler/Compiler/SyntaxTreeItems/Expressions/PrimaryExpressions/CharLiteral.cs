using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class CharLiteral : PrimaryExpression
    {
        public readonly IToken OpenQuote;
        public readonly IToken Text;
        public readonly IToken CloseQuote;
        public CharLiteral(TokenCollection tokens, IToken openQuoteToken)
        {
            OpenQuote = openQuoteToken;
            Text = tokens.PopToken(TokenType.CharLiteral);
            CloseQuote = tokens.PopToken(TokenType.SingleQuote);
        }
        public override string ToString()
        {
            return OpenQuote.ToString() + Text.ToString() + CloseQuote.ToString();
        }
    }
}
