using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PerenthesizedExpression : PrimaryExpression
    {
        public OpenPerenToken OpenPeren { get; private set; }
        public Expression Expression { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }

        public PerenthesizedExpression(TokenCollection tokens, OpenPerenToken? openPeren = null, Expression expression = null, ClosePerenToken? closePeren = null)
        {
            OpenPeren = openPeren == null ? tokens.PopToken<OpenPerenToken>() : (OpenPerenToken)openPeren;
            Expression = expression == null ? Expression.ReadExpression(tokens) : expression;
            ClosePeren = closePeren == null ? tokens.PopToken<ClosePerenToken>() : (ClosePerenToken)closePeren;
        }

        public override string ToString()
        {
            string ret = "";
            ret += OpenPeren.ToString();
            ret += Expression.ToString();
            ret += ClosePeren.ToString();
            return ret;
        }
    }
}
