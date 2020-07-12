using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class PerenthesizedExpression : PrimaryExpression
    {
        public readonly Token OpenPeren;
        public readonly Expression Expression;
        public readonly Token ClosePeren;

        public PerenthesizedExpression(TokenCollection tokens, Token openPeren, Expression expression, Token closePeren)
        {
            OpenPeren = openPeren;
            Expression = expression;
            ClosePeren = closePeren;
        }
        public override string ToString()
        {
            return OpenPeren.ToString() + Expression.ToString() + ClosePeren.ToString();
        }
    }
}
