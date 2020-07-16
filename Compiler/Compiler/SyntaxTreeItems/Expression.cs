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
            switch (tokens.PeekToken())
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
            exprSoFar = EnforcePrecedenceRules(exprSoFar); //Only needed if it is not an assign expression

            bool finishedParsing = false;
            while (!finishedParsing)
            {
                switch (tokens.PeekToken())
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
        private static Expression EnforcePrecedenceRules(Expression expr)
        {
            if (expr is UnaryExpression || expr is SyntaxTreeItems.Type) return expr;

            bool leftAssoc = expr.Precedence != 12 && expr.Precedence != 13 && expr.Precedence != 14;


            bool properPrecedence(Expression upper, Expression lower, bool mayEqual)
            {
                return lower.Precedence < upper.Precedence || (mayEqual && lower.Precedence == upper.Precedence);
            }

            if (properPrecedence(expr, expr.LeftExpr, leftAssoc))
            {
                if (!properPrecedence(expr, expr.RightExpr, !leftAssoc))
                {
                    return RotateLeft(expr);
                }
                else return expr;
            }
            else if (properPrecedence(expr, expr.RightExpr, !leftAssoc))
            {
                if (!properPrecedence(expr, expr.LeftExpr, leftAssoc))
                {
                    return RotateRight(expr);
                }
                else return expr;
            }
            else if (expr.LeftExpr.Precedence == expr.Precedence && !leftAssoc)
            {
                return RotateLeft(RotateRight(expr));
            }
            else if (expr.RightExpr.Precedence == expr.Precedence && leftAssoc)
            {
                return RotateRight(RotateLeft(expr));
            }
            else if (expr.RightExpr.Precedence > expr.Precedence || expr.LeftExpr.Precedence > expr.Precedence) //Only actually need to check one of these
            {
                if(expr.RightExpr.Precedence > expr.LeftExpr.Precedence)
                {
                    return RotateLeft(RotateRight(expr));
                }
                else
                {
                    return RotateRight(RotateLeft(expr));
                }
            }
            else return expr;
        }

        private static Expression RotateLeft(Expression expr)
        {
            bool leftAssoc = expr.Precedence != 12 && expr.Precedence != 13 && expr.Precedence != 14;

            Expression rightestLeft = expr;
            while(rightestLeft.RightExpr.Precedence < expr.Precedence || (rightestLeft.RightExpr.Precedence == expr.Precedence && !leftAssoc))
            {
                rightestLeft = rightestLeft.RightExpr;
            }
            Expression newHead = expr.RightExpr;

            Expression leftestRight = newHead;
            while(expr.Precedence < leftestRight.LeftExpr.Precedence || (expr.Precedence == leftestRight.LeftExpr.Precedence && leftAssoc))
            {
                leftestRight = leftestRight.LeftExpr;
            }
            rightestLeft.RightExpr = leftestRight.LeftExpr;
            leftestRight.LeftExpr = expr;
            return newHead;
        }
        private static Expression RotateRight(Expression expr)
        {
            bool leftAssoc = expr.Precedence != 12 && expr.Precedence != 13 && expr.Precedence != 14;

            Expression leftestRight = expr;
            while (leftestRight.LeftExpr.Precedence < expr.Precedence || (leftestRight.LeftExpr.Precedence == expr.Precedence && leftAssoc))
            {
                leftestRight = leftestRight.LeftExpr;
            }
            Expression newHead = expr.LeftExpr;

            Expression rightestLeft = newHead;
            while (expr.Precedence < rightestLeft.RightExpr.Precedence || (expr.Precedence == rightestLeft.RightExpr.Precedence && !leftAssoc))
            {
                rightestLeft = rightestLeft.RightExpr;
            }
            leftestRight.LeftExpr = rightestLeft.RightExpr;
            rightestLeft.RightExpr = expr;
            return newHead;
        }

        public abstract override string ToString();
    }
}
