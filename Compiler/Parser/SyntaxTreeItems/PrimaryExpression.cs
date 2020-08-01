using System;
using System.Collections.Generic;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public abstract class PrimaryExpression : UnaryExpression
    {
        public static PrimaryExpression ReadPrimaryExpression(TokenCollection tokens)
        {
            PrimaryExpression baseExpr;

            if (tokens.PopIfMatches(out OpenPerenToken openPeren))
            {
                Expression innerExpr = Expression.ReadExpression(tokens);
                if (tokens.PopIfMatches(out ClosePerenToken closePeren))
                {
                    baseExpr = new PerenthesizedExpression(tokens, openPeren, innerExpr, closePeren);
                }
                else if (tokens.PeekToken() is CommaToken)
                {
                    baseExpr = new TupleExpression(tokens, openPeren, new TupleItemList(tokens, new TupleItem(tokens, innerExpr)));
                }
                else throw new InvalidTokenException(tokens);
            }
            else if (tokens.PopIfMatches(out NewKeywordToken newKeyword))
            {
                baseExpr = new NewObjectExpression(tokens, newKeyword);
            }
            else if (tokens.PopIfMatches(out IdentifierToken identifier))
            {
                if (tokens.PopIfMatches(out ColonToken colon))
                {
                    baseExpr = new DeclarationExpression(tokens, identifier, colon);
                }
                else
                {
                    baseExpr = new IdentifierExpression(tokens, identifier);
                }
            }
            else if (tokens.PopIfMatches(out IntLiteralToken intLiteral))
            {
                baseExpr = new IntLiteralExpression(tokens, intLiteral);
            }
            else if (tokens.PopIfMatches(out TrueKeywordToken trueKeyword))
            {
                baseExpr = new TrueLiteralExpression(tokens, trueKeyword);
            }
            else if (tokens.PopIfMatches(out FalseKeywordToken falseKeyword))
            {
                baseExpr = new FalseLiteralExpression(tokens, falseKeyword);
            }
            else if (tokens.PopIfMatches(out DoubleQuoteToken strOpenToken))
            {
                baseExpr = new StringLiteralExpression(tokens, strOpenToken);
            }
            else if (tokens.PopIfMatches(out SingleQuoteToken charOpenToken))
            {
                baseExpr = new CharLiteralExpression(tokens, charOpenToken);
            }
            else if (tokens.PopIfMatches(out ValueKeywordToken valueKeyword))
            {
                baseExpr = new ValueKeywordExpression(tokens, valueKeyword);
            }
            else if (tokens.PopIfMatches(out PrimitiveTypeToken primitiveTypeKeyword))
            {
                baseExpr = new PrimitiveTypeExpression(tokens, primitiveTypeKeyword);
            }
            else throw new InvalidTokenException(tokens);

            PrimaryExpression exprSoFar = baseExpr;
            bool finishedParsing = false;
            while (!finishedParsing && tokens.PeekToken(out IToken token))
            {
                switch (token)
                {
                    case DotToken _: exprSoFar = new MemberAccessExpression(tokens, exprSoFar); break;
                    case NullCondDotToken _: exprSoFar = new NullCondMemberAccessExpression(tokens, exprSoFar); break;
                    case OpenPerenToken _:
                        {
                            if (exprSoFar is MemberAccessExpression||
                                exprSoFar is NullCondArrayAccessExpression ||
                                exprSoFar is IdentifierExpression)
                            {
                                exprSoFar = new MethodCallExpression(tokens, exprSoFar); break;
                            }
                            else finishedParsing = true;
                            break;
                        }
                    case OpenBracketToken _: exprSoFar = new ArrayAccessExpression(tokens, exprSoFar); break;
                    case NullCondOpenBracketToken _: exprSoFar = new NullCondArrayAccessExpression(tokens, exprSoFar); break;
                    case IncrementToken _: exprSoFar = new PostIncrementExpression(tokens, exprSoFar); break;
                    case DecrementToken _: exprSoFar = new PostDecrementExpression(tokens, exprSoFar); break;
                    default: finishedParsing = true; break;
                }
            }
            return exprSoFar;
        }
    }
}
