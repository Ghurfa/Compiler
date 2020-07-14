using Compiler.SyntaxTreeItems.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class StringLiteral : PrimaryExpression
    {
        public readonly IToken OpenQuote;
        public readonly IToken Text;
        public readonly IToken CloseQuote;
        public StringLiteral(TokenCollection tokens, IToken openQuoteToken)
        {
            OpenQuote = openQuoteToken;
            Text = tokens.PopToken(TokenType.StringLiteral);
            CloseQuote = tokens.PopToken(TokenType.DoubleQuote);
        }
        public override string ToString()
        {
            return OpenQuote.ToString() + Text.ToString() + CloseQuote.ToString();
        }
    }
}
