using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public abstract class Expression
    {
        public abstract int Precedence { get; }
        public abstract Expression LeftExpr { get; set; }
        public abstract Expression RightExpr { get; set; }
        public static Expression ReadExpression(TokenCollection tokens)
        {
            UnaryExpression baseExpr = UnaryExpression.ReadUnaryExpression(tokens);

            Expression exprSoFar = baseExpr;
            if (tokens.PeekToken(out IToken token))
            {
                switch (token)
                {
                    case AsKeywordToken _: exprSoFar = new CastExpression(tokens, baseExpr); break;
                    case AssignToken _: exprSoFar = new AssignExpression(tokens, baseExpr); break;
                    case DeclAssignToken _: exprSoFar = new DeclAssignExpression(tokens, baseExpr); break;
                    case PlusAssignToken _: exprSoFar = new PlusAssignExpression(tokens, baseExpr); break;
                    case MinusAssignToken _: exprSoFar = new MinusAssignExpression(tokens, baseExpr); break;
                    case MultiplyAssignToken _: exprSoFar = new MultiplyAssignExpression(tokens, baseExpr); break;
                    case DivideAssignToken _: exprSoFar = new DivideAssignExpression(tokens, baseExpr); break;
                    case ModuloAssignToken _: exprSoFar = new ModuloAssignExpression(tokens, baseExpr); break;
                    case BitwiseAndAssignToken _: exprSoFar = new BitwiseAndAssignExpression(tokens, baseExpr); break;
                    case BitwiseOrAssignToken _: exprSoFar = new BitwiseOrAssignExpression(tokens, baseExpr); break;
                    case BitwiseXorAssignToken _: exprSoFar = new BitwiseXorAssignExpression(tokens, baseExpr); break;
                    case LeftShiftAssignToken _: exprSoFar = new LeftShiftAssignExpression(tokens, baseExpr); break;
                    case RightShiftAssignToken _: exprSoFar = new RightShiftAssignExpression(tokens, baseExpr); break;
                    case NullCoalescingAssignToken _: exprSoFar = new NullCoalescingAssignExpression(tokens, baseExpr); break;
                }
            }

            // Need not enfore precedence rules here

            bool finishedParsing = false;
            while (!finishedParsing && tokens.PeekToken(out token))
            {
                switch (token)
                {
                    case QuestionMarkToken _: exprSoFar = new IfExpression(tokens, exprSoFar); break;
                    case PlusToken _: exprSoFar = new PlusExpression(tokens, exprSoFar); break;
                    case MinusToken _: exprSoFar = new MinusExpression(tokens, exprSoFar); break;
                    case AsteriskToken _: exprSoFar = new MultiplyExpression(tokens, exprSoFar); break;
                    case DivideToken _: exprSoFar = new DivideExpression(tokens, exprSoFar); break;
                    case ModuloToken _: exprSoFar = new ModuloExpression(tokens, exprSoFar); break;
                    case BitwiseAndToken _: exprSoFar = new BitwiseAndExpression(tokens, exprSoFar); break;
                    case BitwiseOrToken _: exprSoFar = new BitwiseOrExpression(tokens, exprSoFar); break;
                    case BitwiseXorToken _: exprSoFar = new BitwiseXorExpression(tokens, exprSoFar); break;
                    case LeftShiftToken _: exprSoFar = new LeftShiftExpression(tokens, exprSoFar); break;
                    case RightShiftToken _: exprSoFar = new RightShiftExpression(tokens, exprSoFar); break;
                    case NullCoalescingToken _: exprSoFar = new NullCoalescingExpression(tokens, exprSoFar); break;
                    case EqualsToken _: exprSoFar = new EqualsExpression(tokens, exprSoFar); break;
                    case NotEqualsToken _: exprSoFar = new NotEqualsExpression(tokens, exprSoFar); break;
                    case GreaterThanToken _: exprSoFar = new GreaterThanExpression(tokens, exprSoFar); break;
                    case LessThanToken _: exprSoFar = new LessThanExpression(tokens, exprSoFar); break;
                    case GreaterThanOrEqualToToken _: exprSoFar = new GreaterThanOrEqualToExpression(tokens, exprSoFar); break;
                    case LessThanOrEqualToToken _: exprSoFar = new LessThanOrEqualToExpression(tokens, exprSoFar); break;
                    case AndToken _: exprSoFar = new AndExpression(tokens, exprSoFar); break;
                    case OrToken _: exprSoFar = new OrExpression(tokens, exprSoFar); break;
                    default: finishedParsing = true; break;
                }
                exprSoFar = EnforcePrecedenceRules(exprSoFar);
            }
            return exprSoFar;
        }
        private static bool ProperPrecedence(Expression upper, Expression lower, bool mayEqual)
        {
            return lower.Precedence < upper.Precedence || (mayEqual && lower.Precedence == upper.Precedence);
        }
        private static Expression EnforcePrecedenceRules(Expression expr)
        {
            if (expr is UnaryExpression || expr is SyntaxTreeItems.Type) return expr;

            bool leftAssoc = expr.Precedence != 12 && expr.Precedence != 13 && expr.Precedence != 14;

            if (!ProperPrecedence(expr, expr.RightExpr, !leftAssoc))
            {
                return RotateLeft(expr);
            }
            else return expr;
        }

        private static Expression RotateLeft(Expression expr)
        {
            bool leftAssoc = expr.Precedence != 12 && expr.Precedence != 13 && expr.Precedence != 14;

            Expression newHead = expr.RightExpr;

            Expression leftestRight = newHead;
            while (ProperPrecedence(leftestRight.LeftExpr, expr, leftAssoc))
            {
                leftestRight = leftestRight.LeftExpr;
            }
            expr.RightExpr = leftestRight.LeftExpr;
            leftestRight.LeftExpr = expr;
            return newHead;
        }
        public abstract override string ToString();
    }
}
