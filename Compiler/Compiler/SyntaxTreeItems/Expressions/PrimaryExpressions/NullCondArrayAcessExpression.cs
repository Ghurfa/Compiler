using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class NullCondArrayAcessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression Array;
        public readonly Token NullCondOpenBracket;
        public readonly Expression Index;
        public readonly Token CloseBracket;

        public NullCondArrayAcessExpression(TokenCollection tokens, PrimaryExpression baseExpr)
        {
            Array = baseExpr;
            NullCondOpenBracket = tokens.PopToken(TokenType.NullCondOpenBracket);
            Index = Expression.ReadExpression(tokens);
            CloseBracket = tokens.PopToken(TokenType.CloseBracket);
        }
        public override string ToString()
        {
            return Array.ToString() + NullCondOpenBracket.ToString() + Index.ToString() + CloseBracket.ToString();
        }
    }
}
