using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class MethodCallExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly PrimaryExpression Method;
        public readonly OpenPerenToken OpenPeren;
        public readonly ArgumentList Arguments;
        public readonly ClosePerenToken ClosePeren;

        public override IToken LeftToken => Method.LeftToken;
        public override IToken RightToken => ClosePeren;

        public MethodCallExpression(TokenCollection tokens, PrimaryExpression method = null, OpenPerenToken? openPeren = null, ArgumentList arguments = null, ClosePerenToken? closePeren = null)
        {
            Method = method == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : method;
            OpenPeren = openPeren == null ? tokens.PopToken<OpenPerenToken>() : (OpenPerenToken)openPeren;
            Arguments = arguments == null ? new ArgumentList(tokens) : arguments;
            ClosePeren = closePeren == null ? tokens.PopToken<ClosePerenToken>() : (ClosePerenToken)closePeren;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            
            return ret;
        }
    }
}
