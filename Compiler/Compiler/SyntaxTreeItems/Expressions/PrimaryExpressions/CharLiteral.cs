using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class CharLiteral : PrimaryExpression
    {
        public readonly Token OpenQuote;
        public readonly Token Text;
        public readonly Token CloseQuote;
        public CharLiteral(TokenCollection tokens, Token openQuoteToken)
        {
            OpenQuote = openQuoteToken;
            Text = tokens.PopToken(TokenType.CharLiteral);
            CloseQuote = tokens.PopToken(TokenType.SingleQuote);
        }
    }
}
