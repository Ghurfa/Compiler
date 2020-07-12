using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class AssignmentExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression LeftExpression;
        public readonly Token Assignment;
        public readonly Expression RightExpression;
        public AssignmentExpression(TokenCollection tokens, UnaryExpression left)
        {
            LeftExpression = left;
            Assignment = tokens.PopToken();
            RightExpression = Expression.ReadExpression(tokens);
        }
    }
    public class AssignExpression : AssignmentExpression
    {
        public AssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class DeclAssignExpression : AssignmentExpression
    {
        public DeclAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class PlusAssignExpression : AssignmentExpression
    {
        public PlusAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class MinusAssignExpression : AssignmentExpression
    {
        public MinusAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class TimesAssignExpression : AssignmentExpression
    {
        public TimesAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class DivideAssignExpression : AssignmentExpression
    {
        public DivideAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class ModuloAssignExpression : AssignmentExpression
    {
        public ModuloAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class BitwiseAndAssignExpression : AssignmentExpression
    {
        public BitwiseAndAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class BitwiseOrAssignExpression : AssignmentExpression
    {
        public BitwiseOrAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class BitwiseXorAssignExpression : AssignmentExpression
    {
        public BitwiseXorAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class LeftShiftAssignExpression : AssignmentExpression
    {
        public LeftShiftAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class RightShiftAssignExpression : AssignmentExpression
    {
        public RightShiftAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
    public class NullCoalescingAssignExpression : AssignmentExpression
    {
        public NullCoalescingAssignExpression(TokenCollection tokens, UnaryExpression left) : base(tokens, left) { }
    }
}
