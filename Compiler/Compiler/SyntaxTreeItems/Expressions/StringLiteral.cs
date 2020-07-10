using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class StringLiteral : Expression
    {
        public readonly Token OpenQuote;
        public readonly Token Text;
        public readonly Token CloseQuote;
        public StringLiteral(LinkedList<Token> tokens, Token openQuoteToken)
        {
            OpenQuote = openQuoteToken;
            Text = tokens.GetToken(TokenType.StringLiteral);
            CloseQuote = tokens.GetToken(TokenType.SyntaxChar, "\"");
        }
    }
}
