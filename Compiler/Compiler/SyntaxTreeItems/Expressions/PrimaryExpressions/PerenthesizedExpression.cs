using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class PerenthesizedExpression : PrimaryExpression
    {
        public readonly OpenPerenToken OpenPeren;
        public readonly Expression Expression;
        public readonly ClosePerenToken ClosePeren;

        public override IToken LeftToken => OpenPeren;
        public override IToken RightToken => ClosePeren;

        public PerenthesizedExpression(TokenCollection tokens, OpenPerenToken? openPeren = null, Expression expression = null, ClosePerenToken? closePeren = null)
        {
            OpenPeren = openPeren == null ? tokens.PopToken<OpenPerenToken>() : (OpenPerenToken)openPeren;
            Expression = expression == null ? Expression.ReadExpression(tokens) : expression;
            ClosePeren = closePeren == null ? tokens.PopToken<ClosePerenToken>() : (ClosePerenToken)closePeren;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
