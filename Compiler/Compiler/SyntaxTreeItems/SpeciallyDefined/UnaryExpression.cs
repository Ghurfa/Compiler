using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class UnaryExpression : Expression
    {
        public static UnaryExpression ReadUnaryExpression(TokenCollection tokens)
        {
            if (tokens.PopIfMatches(out IToken plusToken, TokenType.Plus))
            {
                return new UnaryPlusExpression(tokens, plusToken);
            }
            else if (tokens.PopIfMatches(out IToken minusToken, TokenType.Minus))
            {
                return new UnaryMinusExpression(tokens, minusToken);
            }
            else if (tokens.PopIfMatches(out IToken incrementToken, TokenType.Increment))
            {
                return new PreIncrementExpression(tokens, incrementToken);
            }
            else if (tokens.PopIfMatches(out IToken decrementToken, TokenType.Decrement))
            {
                return new PreDecrementExpression(tokens, decrementToken);
            }
            else if (tokens.PopIfMatches(out IToken notToken, TokenType.Not))
            {
                return new LogicalNotExpression(tokens, notToken);
            }
            else if (tokens.PopIfMatches(out IToken bitwiseNotToken, TokenType.BitwiseNot))
            {
                return new BitwiseNotExpression(tokens, bitwiseNotToken);
            }
            else if (tokens.PopIfMatches(out IToken derefToken, TokenType.Asterisk))
            {
                return new DereferenceExpression(tokens, derefToken);
            }
            else return PrimaryExpression.ReadPrimaryExpression(tokens);
        }
    }
}
