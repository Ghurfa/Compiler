using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MethodCallExpression : PrimaryExpression, ICompleteStatement
    {
        public PrimaryExpression Method { get; private set; }
        public OpenPerenToken OpenPeren { get; private set; }
        public ArgumentList Arguments { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }

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
            ret += Method.ToString();
            ret += " ";
            ret += OpenPeren.ToString();
            ret += Arguments.ToString();
            ret += ClosePeren.ToString();
            return ret;
        }
    }
}
