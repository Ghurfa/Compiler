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
            switch(tokens.PeekToken().Type)
            {
                case TokenType.AsKeyword: exprSoFar = new CastExpression(tokens, baseExpr); break;
                case TokenType.Assign: exprSoFar = new AssignExpression(tokens, baseExpr); break;
                case TokenType.DeclAssign: exprSoFar = new DeclAssignExpression(tokens, baseExpr); break;
                case TokenType.PlusAssign: exprSoFar = new PlusAssignExpression(tokens, baseExpr); break;
                case TokenType.MinusAssign: exprSoFar = new MinusAssignExpression(tokens, baseExpr); break;
                case TokenType.TimesAssign: exprSoFar = new TimesAssignExpression(tokens, baseExpr); break;
                case TokenType.DivideAssign: exprSoFar = new DivideAssignExpression(tokens, baseExpr); break;
                case TokenType.ModuloAssign: exprSoFar = new ModuloAssignExpression(tokens, baseExpr); break;
                case TokenType.BitwiseAndAssign: exprSoFar = new BitwiseAndAssignExpression(tokens, baseExpr); break;
                case TokenType.BitwiseOrAssign: exprSoFar = new BitwiseOrAssignExpression(tokens, baseExpr); break;
                case TokenType.BitwiseXorAssign: exprSoFar = new BitwiseXorAssignExpression(tokens, baseExpr); break;
                case TokenType.LeftShiftAssign: exprSoFar = new LeftShiftAssignExpression(tokens, baseExpr); break;
                case TokenType.RightShiftAssign: exprSoFar = new RightShiftAssignExpression(tokens, baseExpr); break;
                case TokenType.NullCoalescingAssign: exprSoFar = new NullCoalescingAssignExpression(tokens, baseExpr); break;
            }

            bool finishedParsing = false;
            while (!finishedParsing)
            {
                switch (tokens.PeekToken().Type)
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
                    case TokenType.NullCoalescing: exprSoFar = new NullCoalescingExpression(tokens, exprSoFar); break;
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
        public abstract override string ToString();
    }
}
