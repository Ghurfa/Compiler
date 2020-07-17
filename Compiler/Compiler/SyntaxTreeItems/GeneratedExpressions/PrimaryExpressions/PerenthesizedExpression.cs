using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PerenthesizedExpression : PrimaryExpression
    {
        public OpenPerenToken OpenPeren { get; private set; }
        public Expression Expression { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }

        public PerenthesizedExpression(TokenCollection tokens, OpenPerenToken openPeren, Expression expression, ClosePerenToken closePeren)
        {
            OpenPeren = openPeren;
            Expression = expression;
            ClosePeren = closePeren;
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
