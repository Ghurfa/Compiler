using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class UnaryOperatorExpression : UnaryExpression
    {
        public readonly Token UnaryOperator;
        public readonly UnaryExpression Expression;
        public UnaryOperatorExpression(TokenCollection tokens, Token unaryOperator)
        {
            UnaryOperator = unaryOperator;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }
    }
    public class BitwiseNotExpression : UnaryOperatorExpression
    {
        public BitwiseNotExpression(TokenCollection tokens, Token unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class DereferenceExpression : UnaryOperatorExpression
    {
        public DereferenceExpression(TokenCollection tokens, Token unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class LogicalNotExpression : UnaryOperatorExpression
    {
        public LogicalNotExpression(TokenCollection tokens, Token unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class PreIncrementExpression : UnaryOperatorExpression
    {
        public PreIncrementExpression(TokenCollection tokens, Token unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class PreDecrementExpression : UnaryOperatorExpression
    {
        public PreDecrementExpression(TokenCollection tokens, Token unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class UnaryPlusExpression : UnaryOperatorExpression
    {
        public UnaryPlusExpression(TokenCollection tokens, Token unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class UnaryMinusExpression : UnaryOperatorExpression
    {
        public UnaryMinusExpression(TokenCollection tokens, Token unaryOperator) : base(tokens, unaryOperator) { }
    }
}
