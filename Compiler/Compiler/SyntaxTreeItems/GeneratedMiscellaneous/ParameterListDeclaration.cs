using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ParameterListDeclaration
    {
        public OpenPerenToken OpenPeren { get; private set; }
        public ParameterDeclaration[] Parameters { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }

        public ParameterListDeclaration(TokenCollection tokens)
        {
            OpenPeren = tokens.PopToken<OpenPerenToken>();
            LinkedList<ParameterDeclaration> parametersList = new LinkedList<ParameterDeclaration>();
            bool lastMissingComma = tokens.PeekToken() is ClosePerenToken;
            while (!lastMissingComma)
            {
                var add = new ParameterDeclaration(tokens);
                parametersList.AddLast(add);
                lastMissingComma = add.Comma == null;
            }
            Parameters = parametersList.ToArray();
            ClosePeren = tokens.PopToken<ClosePerenToken>();
        }

        public override string ToString()
        {
            string ret = "";
            ret += OpenPeren.ToString();
            foreach (var item in Parameters)
            {
                ret += item.ToString();
            }
            ret += ClosePeren.ToString();
            return ret;
        }
    }
}
