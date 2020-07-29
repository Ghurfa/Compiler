using Parser.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using Tokenizer;

namespace Parser
{
    public abstract class Expression
    {
        public abstract int Precedence { get; }
        public abstract Expression LeftExpr { get; set; }
        public abstract Expression RightExpr { get; set; }

        public static Expression ReadExpression(TokenCollection tokens)
        {
            UnaryExpression baseExpr = UnaryExpression.ReadUnaryExpression(tokens);

            if (tokens.PeekToken(out IToken token))
            {
                Expression expr;
                switch (token)
                {
                    case AsKeywordToken _: expr = new CastExpression(tokens, baseExpr); break;
                    case AssignToken _: expr = new AssignExpression(tokens, baseExpr); break;
                    case DeclAssignToken _: expr = new DeclAssignExpression(tokens, baseExpr); break;
                    case PlusAssignToken _: expr = new PlusAssignExpression(tokens, baseExpr); break;
                    case MinusAssignToken _: expr = new MinusAssignExpression(tokens, baseExpr); break;
                    case MultiplyAssignToken _: expr = new MultiplyAssignExpression(tokens, baseExpr); break;
                    case DivideAssignToken _: expr = new DivideAssignExpression(tokens, baseExpr); break;
                    case ModuloAssignToken _: expr = new ModuloAssignExpression(tokens, baseExpr); break;
                    case BitwiseAndAssignToken _: expr = new BitwiseAndAssignExpression(tokens, baseExpr); break;
                    case BitwiseOrAssignToken _: expr = new BitwiseOrAssignExpression(tokens, baseExpr); break;
                    case BitwiseXorAssignToken _: expr = new BitwiseXorAssignExpression(tokens, baseExpr); break;
                    case LeftShiftAssignToken _: expr = new LeftShiftAssignExpression(tokens, baseExpr); break;
                    case RightShiftAssignToken _: expr = new RightShiftAssignExpression(tokens, baseExpr); break;
                    case NullCoalescingAssignToken _: expr = new NullCoalescingAssignExpression(tokens, baseExpr); break;
                    case QuestionMarkToken _: expr = new IfExpression(tokens, baseExpr); break;
                    case PlusToken _: expr = new PlusExpression(tokens, baseExpr); break;
                    case MinusToken _: expr = new MinusExpression(tokens, baseExpr); break;
                    case AsteriskToken _: expr = new MultiplyExpression(tokens, baseExpr); break;
                    case DivideToken _: expr = new DivideExpression(tokens, baseExpr); break;
                    case ModuloToken _: expr = new ModuloExpression(tokens, baseExpr); break;
                    case BitwiseAndToken _: expr = new BitwiseAndExpression(tokens, baseExpr); break;
                    case BitwiseOrToken _: expr = new BitwiseOrExpression(tokens, baseExpr); break;
                    case BitwiseXorToken _: expr = new BitwiseXorExpression(tokens, baseExpr); break;
                    case LeftShiftToken _: expr = new LeftShiftExpression(tokens, baseExpr); break;
                    case RightShiftToken _: expr = new RightShiftExpression(tokens, baseExpr); break;
                    case NullCoalescingToken _: expr = new NullCoalescingExpression(tokens, baseExpr); break;
                    case EqualsToken _: expr = new EqualsExpression(tokens, baseExpr); break;
                    case NotEqualsToken _: expr = new NotEqualsExpression(tokens, baseExpr); break;
                    case GreaterThanToken _: expr = new GreaterThanExpression(tokens, baseExpr); break;
                    case LessThanToken _: expr = new LessThanExpression(tokens, baseExpr); break;
                    case GreaterThanOrEqualToToken _: expr = new GreaterThanOrEqualToExpression(tokens, baseExpr); break;
                    case LessThanOrEqualToToken _: expr = new LessThanOrEqualToExpression(tokens, baseExpr); break;
                    case AndToken _: expr = new AndExpression(tokens, baseExpr); break;
                    case OrToken _: expr = new OrExpression(tokens, baseExpr); break;
                    default: return baseExpr;
                }
                return EnforcePrecedenceRules(expr);
            }
            else return baseExpr;
        }

        private static bool ProperPrecedence(Expression upper, Expression lower, bool mayEqual)
        {
            return lower.Precedence < upper.Precedence || (mayEqual && lower.Precedence == upper.Precedence);
        }

        private static Expression EnforcePrecedenceRules(Expression expr)
        {
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
