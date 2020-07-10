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
        public CharLiteral(LinkedList<Token> tokens, Token openQuoteToken)
        {
            OpenQuote = openQuoteToken;
            Text = tokens.GetToken(TokenType.CharLiteral);
            CloseQuote = tokens.GetToken(TokenType.SyntaxChar, "\'");
        }
    }
}
