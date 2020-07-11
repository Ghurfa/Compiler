using Compiler.SyntaxTreeItems.Expressions.UnaryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class UnaryExpression : Expression
    {
        public static UnaryExpression ReadUnaryExpression(TokenCollection tokens)
        {
            if (tokens.PopIfMatches(out Token plusToken, TokenType.Plus))
            {
                return new UnaryPlusExpression(tokens, plusToken);
            }
            else if (tokens.PopIfMatches(out Token minusToken, TokenType.Minus))
            {
                return new UnaryMinusExpression(tokens, minusToken);
            }
            else if (tokens.PopIfMatches(out Token incrementToken, TokenType.Increment))
            {
                return new PreIncrementExpression(tokens, incrementToken);
            }
            else if (tokens.PopIfMatches(out Token decrementToken, TokenType.Decrement))
            {
                return new PreDecrementExpression(tokens, decrementToken);
            }
            else if (tokens.PopIfMatches(out Token notToken, TokenType.Not))
            {
                return new LogicalNotExpression(tokens, notToken);
            }
            else if (tokens.PopIfMatches(out Token bitwiseNotToken, TokenType.BitwiseNot))
            {
                return new BitwiseNotExpression(tokens, bitwiseNotToken);
            }
            else if (tokens.PopIfMatches(out Token derefToken, TokenType.Asterisk))
            {
                return new DereferenceExpression(tokens, derefToken);
            }
            else return PrimaryExpression.ReadPrimaryExpression(tokens);
        }
    }
}
