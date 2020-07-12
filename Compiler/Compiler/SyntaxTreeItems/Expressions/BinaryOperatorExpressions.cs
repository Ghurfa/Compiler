using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class BinaryOperatorExpression : Expression
    {
        public readonly Expression Left;
        public readonly Token BinaryOperator;
        public readonly Expression Right;
        public BinaryOperatorExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            BinaryOperator = tokens.PopToken();
            Right = Expression.ReadExpression(tokens);
        }
    }
    public class PlusExpression : BinaryOperatorExpression
    {
        public PlusExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class MinusExpression : BinaryOperatorExpression
    {
        public MinusExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class TimesExpression : BinaryOperatorExpression
    {
        public TimesExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class DivideExpression : BinaryOperatorExpression
    {
        public DivideExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class ModuloExpression : BinaryOperatorExpression
    {
        public ModuloExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class BitwiseAndExpression : BinaryOperatorExpression
    {
        public BitwiseAndExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class BitwiseOrExpression : BinaryOperatorExpression
    {
        public BitwiseOrExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class BitwiseXorExpression : BinaryOperatorExpression
    {
        public BitwiseXorExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class LeftShiftExpression : BinaryOperatorExpression
    {
        public LeftShiftExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class RightShiftExpression : BinaryOperatorExpression
    {
        public RightShiftExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class NullCoalescingExpression : BinaryOperatorExpression
    {
        public NullCoalescingExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class EqualsExpression : BinaryOperatorExpression
    {
        public EqualsExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class NotEqualsExpression : BinaryOperatorExpression
    {
        public NotEqualsExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class GreaterThanExpression : BinaryOperatorExpression
    {
        public GreaterThanExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class LessThanExpression : BinaryOperatorExpression
    {
        public LessThanExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class GreaterThanOrEqualToExpression : BinaryOperatorExpression
    {
        public GreaterThanOrEqualToExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class LessThanOrEqualToExpression : BinaryOperatorExpression
    {
        public LessThanOrEqualToExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class AndExpression : BinaryOperatorExpression
    {
        public AndExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
    public class OrExpression : BinaryOperatorExpression
    {
        public OrExpression(TokenCollection tokens, Expression left) : base(tokens, left) { }
    }
}
