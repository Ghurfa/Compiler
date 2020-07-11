using Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class PrimaryExpression : UnaryExpression
    {
        public static PrimaryExpression ReadPrimaryExpression(TokenCollection tokens)
        {
            PrimaryExpression baseExpr;

            if (tokens.PopIfMatches(out Token openPeren, TokenType.OpenPeren))
            {
                Expression innerExpr = Expression.ReadExpression(tokens);
                if (tokens.PopIfMatches(out Token closePeren, TokenType.ClosePeren))
                {
                    baseExpr = new PerenthesizedExpression(tokens, openPeren, innerExpr, closePeren);
                }
                else if (tokens.PopIfMatches(out Token comma, TokenType.ClosePeren))
                {
                    baseExpr = new TupleExpression(tokens, openPeren, innerExpr, comma);
                }
                else throw new SyntaxTreeBuildingException();
            }
            else if (tokens.PopIfMatches(out Token newKeyword, TokenType.NewKeyword))
            {
                baseExpr = new NewObjectExpression(tokens, newKeyword);
            }
            else if (tokens.PopIfMatches(out Token identifier, TokenType.Identifier))
            {
                if(tokens.PopIfMatches(out Token colonToken, TokenType.Colon))
                {
                    baseExpr = new DeclarationExpression(tokens, identifier, colonToken);
                }
                else
                {
                    baseExpr = new IdentifierExpression(tokens, identifier);
                }
            }
            else if (tokens.PopIfMatches(out Token intToken, TokenType.IntLiteral))
            {
                baseExpr = new IntLiteral(tokens, intToken);
            }
            else if (tokens.PopIfMatches(out Token strOpenToken, TokenType.DoubleQuote))
            {
                baseExpr = new StringLiteral(tokens, strOpenToken);
            }
            else if (tokens.PopIfMatches(out Token charOpenToken, TokenType.SingleQuote))
            {
                baseExpr = new CharLiteral(tokens, charOpenToken);
            }
            else if (tokens.PopIfMatches(out Token valueKeyword, TokenType.ValueKeyword))
            {
                baseExpr = new ValueKeywordExpression(tokens, valueKeyword);
            }
            else if (tokens.PopIfMatches(out Token primitiveKeyword, TokenType.PrimitiveType))
            {
                baseExpr = new PrimitiveTypeExpression(tokens, primitiveKeyword);
            }
            else throw new SyntaxTreeBuildingException(tokens.PeekToken());

            PrimaryExpression exprSoFar = baseExpr;
            bool finishedParsing = false;
            while (!finishedParsing)
            {
                if (tokens.PopIfMatches(out Token dot, TokenType.Dot))
                {
                    exprSoFar = new MemberAccessExpression(tokens, exprSoFar, dot);
                }
                else if (tokens.PopIfMatches(out Token openPerentheses, TokenType.OpenPeren))
                {
                    exprSoFar = new MethodCallExpression(tokens, exprSoFar, openPerentheses);
                }
                else if (tokens.PopIfMatches(out Token openArrBracket, TokenType.OpenBracket))
                {
                    throw new NotImplementedException();
                }
                else if (tokens.PopIfMatches(out Token incrOperator, TokenType.Increment))
                {
                    exprSoFar = new PostIncrementExpression(tokens, exprSoFar, incrOperator);
                }
                else if (tokens.PopIfMatches(out Token decrOperator, TokenType.Decrement))
                {
                    exprSoFar = new PostDecrementExpression(tokens, exprSoFar, decrOperator);
                }
                else finishedParsing = true;
            }
            return exprSoFar;
        }
    }
}
