using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ParameterListDeclaration
    {
        public readonly OpenPerenToken OpenPeren;
        public readonly ParameterDeclaration[] Parameters;
        public readonly ClosePerenToken ClosePeren;

        public  IToken LeftToken => OpenPeren;
        public  IToken RightToken => ClosePeren;

        public ParameterListDeclaration(TokenCollection tokens, OpenPerenToken? openPeren = null, ParameterDeclaration[] parameters = null)
        {
            OpenPeren = openPeren == null ? tokens.PopToken<OpenPerenToken>() : (OpenPerenToken)openPeren;
            var parametersList = new LinkedList<ParameterDeclaration>();
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    parametersList.AddLast(item);
                }
            }
            while(!tokens.PopIfMatches(out ClosePeren))
            {
                var newItem = new ParameterDeclaration(tokens);
                parametersList.AddLast(newItem);
            }
            Parameters = parametersList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
