using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class UnaryOperatorExpression : UnaryExpression
    {
        public readonly IToken UnaryOperator;
        public readonly UnaryExpression Expression;
        public UnaryOperatorExpression(TokenCollection tokens, IToken unaryOperator)
        {
            UnaryOperator = unaryOperator;
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
        }
        public override string ToString()
        {
            return UnaryOperator.ToString() + Expression.ToString();
        }
    }
    public class BitwiseNotExpression : UnaryOperatorExpression
    {
        public BitwiseNotExpression(TokenCollection tokens, IToken unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class DereferenceExpression : UnaryOperatorExpression
    {
        public DereferenceExpression(TokenCollection tokens, IToken unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class LogicalNotExpression : UnaryOperatorExpression
    {
        public LogicalNotExpression(TokenCollection tokens, IToken unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class PreIncrementExpression : UnaryOperatorExpression
    {
        public PreIncrementExpression(TokenCollection tokens, IToken unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class PreDecrementExpression : UnaryOperatorExpression
    {
        public PreDecrementExpression(TokenCollection tokens, IToken unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class UnaryPlusExpression : UnaryOperatorExpression
    {
        public UnaryPlusExpression(TokenCollection tokens, IToken unaryOperator) : base(tokens, unaryOperator) { }
    }
    public class UnaryMinusExpression : UnaryOperatorExpression
    {
        public UnaryMinusExpression(TokenCollection tokens, IToken unaryOperator) : base(tokens, unaryOperator) { }
    }
}
