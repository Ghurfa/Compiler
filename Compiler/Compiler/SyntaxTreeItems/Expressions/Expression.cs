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
                exprSoFar = new AssignExpression(tokens, baseExpr, assign);
            }
            else if (tokens.PopIfMatches(out Token declAssign, TokenType.DeclAssign))
            {
                exprSoFar = new DeclAssignExpression(tokens, baseExpr, declAssign);
            }
            else if (tokens.PopIfMatches(out Token plusAssign, TokenType.PlusAssign))
            {
                exprSoFar = new PlusAssignExpression(tokens, baseExpr, plusAssign);
            }
            else if (tokens.PopIfMatches(out Token minusAssign, TokenType.MinusAssign))
            {
                exprSoFar = new MinusAssignExpression(tokens, baseExpr, minusAssign);
            }
            else if (tokens.PopIfMatches(out Token timesAssign, TokenType.TimesAssign))
            {
                exprSoFar = new TimesAssignExpression(tokens, baseExpr, timesAssign);
            }
            else if (tokens.PopIfMatches(out Token divideAssign, TokenType.DivideAssign))
            {
                exprSoFar = new DivideAssignExpression(tokens, baseExpr, divideAssign);
            }
            else if (tokens.PopIfMatches(out Token moduloAssign, TokenType.ModuloAssign))
            {
                exprSoFar = new ModuloAssignExpression(tokens, baseExpr, moduloAssign);
            }

            bool finishedParsing = false;
            while (!finishedParsing)
            {
                Token nextToken = tokens.PeekToken();
                switch (nextToken.Type)
                {
                    case TokenType.QuestionMark: exprSoFar = new InlineIfExpression(tokens, exprSoFar); break;
                    case TokenType.Plus: exprSoFar = new PlusExpression(tokens, exprSoFar); break;
                    case TokenType.Minus: exprSoFar = new MinusExpression(tokens, exprSoFar); break;
                    case TokenType.Asterisk: exprSoFar = new TimesExpression(tokens, exprSoFar); break;
                    case TokenType.Divide: exprSoFar = new DivideExpression(tokens, exprSoFar); break;
                    case TokenType.Modulo: exprSoFar = new ModuloExpression(tokens, exprSoFar); break;
                    case TokenType.BitwiseAnd: exprSoFar = new BitwiseAndExpression(tokens, exprSoFar); break;
                    case TokenType.BitwiseOr: exprSoFar = new BitwiseOrExpression(tokens, exprSoFar); break;
                    case TokenType.BitwiseXor: exprSoFar = new BitwiseXorExpression(tokens, exprSoFar); break;
                    case TokenType.LeftShift: exprSoFar = new LeftShiftExpression(tokens, exprSoFar); break;
                    case TokenType.RightShift: exprSoFar = new RightShiftExpression(tokens, exprSoFar); break;
                    case TokenType.Equals: exprSoFar = new EqualsExpression(tokens, exprSoFar); break;
                    case TokenType.NotEquals: exprSoFar = new NotEqualsExpression(tokens, exprSoFar); break;
                    case TokenType.GreaterThan: exprSoFar = new GreaterThanExpression(tokens, exprSoFar); break;
                    case TokenType.LessThan: exprSoFar = new LessThanExpression(tokens, exprSoFar); break;
                    case TokenType.GreaterThanOrEqualTo: exprSoFar = new GreaterThanOrEqualToExpression(tokens, exprSoFar); break;
                    case TokenType.LessThanOrEqualTo: exprSoFar = new LessThanOrEqualToExpression(tokens, exprSoFar); break;
                    case TokenType.And: exprSoFar = new AndExpression(tokens, exprSoFar); break;
                    case TokenType.Or: exprSoFar = new OrExpression(tokens, exprSoFar); break;
                    default: finishedParsing = true; break;
                }
            }
            return exprSoFar;
        }
    }
}
