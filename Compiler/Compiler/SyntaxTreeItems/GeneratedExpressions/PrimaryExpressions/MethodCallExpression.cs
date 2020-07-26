using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MethodCallExpression : PrimaryExpression, ICompleteStatement
    {
        public PrimaryExpression Method { get; private set; }
        public OpenPerenToken OpenPeren { get; private set; }
        public ArgumentList Arguments { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }

        public MethodCallExpression(TokenCollection tokens, PrimaryExpression method)
        {
            Method = method;
            OpenPeren = tokens.PopToken<OpenPerenToken>();
            Arguments = new ArgumentList(tokens);
            ClosePeren = tokens.PopToken<ClosePerenToken>();
        }

        public override string ToString()
        {
            string ret = "";
            ret += Method.ToString();
            ret += OpenPeren.ToString();
            ret += Arguments.ToString();
            ret += ClosePeren.ToString();
            return ret;
        }
    }
}
