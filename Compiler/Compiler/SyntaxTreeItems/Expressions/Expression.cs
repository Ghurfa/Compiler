using Compiler.SyntaxTreeItems.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public abstract class Expression
    {
        public static Expression ReadExpression(TokenCollection tokens)
        {
            UnaryExpression baseExpr = UnaryExpression.ReadUnaryExpression(tokens);

            Expression exprSoFar = baseExpr;
            if (tokens.PopIfMatches(out Token asKeyword, TokenType.AsKeyword))
            {
                exprSoFar = new CastExpression(tokens, baseExpr, asKeyword);
            }
            else if (tokens.PopIfMatches(out Token assign, TokenType.Assign))
            {

            }
            else if (tokens.PopIfMatches(out Token declAssign, TokenType.DeclAssign))
            {

            }
            else if (tokens.PopIfMatches(out Token plusAssign, TokenType.PlusAssign))
            {

            }
            else if (tokens.PopIfMatches(out Token minusAssign, TokenType.MinusAssign))
            {

            }
            else if (tokens.PopIfMatches(out Token timesAssign, TokenType.TimesAssign))
            {

            }
            else if (tokens.PopIfMatches(out Token divideAssign, TokenType.DivideAssign))
            {

            }
            else if (tokens.PopIfMatches(out Token moduloAssign, TokenType.ModuloAssign))
            {

            }
            bool finishedParsing = false;
            while (!finishedParsing)
            {
                if (tokens.PopIfMatches(out Token questionMark, TokenType.QuestionMark))
                {
                    exprSoFar = new InlineIfExpression(tokens, exprSoFar, questionMark);
                }
                else finishedParsing = true;
            }
            return exprSoFar;
        }
    }
}
