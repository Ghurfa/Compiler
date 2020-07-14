using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class PerenthesizedExpression : PrimaryExpression
    {
        public readonly IToken OpenPeren;
        public readonly Expression Expression;
        public readonly IToken ClosePeren;

        public PerenthesizedExpression(TokenCollection tokens, IToken openPeren, Expression expression, IToken closePeren)
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
