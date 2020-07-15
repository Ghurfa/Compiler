using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class PerenthesizedExpression : PrimaryExpression
    {
        public readonly OpenPerenToken OpenPeren;
        public readonly Expression Expression;
        public readonly ClosePerenToken ClosePeren;

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
