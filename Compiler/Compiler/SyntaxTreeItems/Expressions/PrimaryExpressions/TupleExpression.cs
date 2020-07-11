using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class TupleExpression : PrimaryExpression
    {
        public readonly Token OpenPerentheses;
        public readonly TupleValueList Values;
        public readonly Token ClosePerentheses;

        public TupleExpression(TokenCollection tokens, Token openPerens, Expression firstValue, Token firstComma)
        {
            OpenPerentheses = openPerens;
            Values = new TupleValueList(tokens, new TupleValue(firstValue, firstComma));
            ClosePerentheses = tokens.PopToken(TokenType.ClosePeren);
        }
    }
}
