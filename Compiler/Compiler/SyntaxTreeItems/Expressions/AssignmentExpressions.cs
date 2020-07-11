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
        public AssignmentExpression(TokenCollection tokens, UnaryExpression left, Token assignToken)
        {
            LeftExpression = left;
            Assignment = assignToken;
            RightExpression = Expression.ReadExpression(tokens);
        }
    }
    public class AssignExpression : AssignmentExpression
    {
        public AssignExpression(TokenCollection tokens, UnaryExpression left, Token assignToken) : base(tokens, left, assignToken) { }
    }
    public class DeclAssignExpression : AssignmentExpression
    {
        public DeclAssignExpression(TokenCollection tokens, UnaryExpression left, Token assignToken) : base(tokens, left, assignToken) { }
    }
    public class PlusAssignExpression : AssignmentExpression
    {
        public PlusAssignExpression(TokenCollection tokens, UnaryExpression left, Token assignToken) : base(tokens, left, assignToken) { }
    }
    public class MinusAssignExpression : AssignmentExpression
    {
        public MinusAssignExpression(TokenCollection tokens, UnaryExpression left, Token assignToken) : base(tokens, left, assignToken) { }
    }
    public class TimesAssignExpression : AssignmentExpression
    {
        public TimesAssignExpression(TokenCollection tokens, UnaryExpression left, Token assignToken) : base(tokens, left, assignToken) { }
    }
    public class DivideAssignExpression : AssignmentExpression
    {
        public DivideAssignExpression(TokenCollection tokens, UnaryExpression left, Token assignToken) : base(tokens, left, assignToken) { }
    }
    public class ModuloAssignExpression : AssignmentExpression
    {
        public ModuloAssignExpression(TokenCollection tokens, UnaryExpression left, Token assignToken) : base(tokens, left, assignToken) { }
    }
}
