using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace Compiler.SyntaxTreeItems
{
    public class ParameterListDeclaration
    {
        public readonly OpenPerenToken OpenPeren;
        public readonly ParameterDeclaration[] Parameters;
        public readonly ClosePerenToken ClosePeren;
        public ParameterListDeclaration(TokenCollection tokens)
        {
            OpenPeren = tokens.PopToken<OpenPerenToken>();
            LinkedList<ParameterDeclaration> parameters = new LinkedList<ParameterDeclaration>();
            bool lastMissingComma = false;
            while (!tokens.PopIfMatches(out ClosePeren))
            {
                if (lastMissingComma) throw new MissingCommaException(tokens);
                var parameter = new ParameterDeclaration(tokens);
                lastMissingComma = parameter.Comma == null;
                parameters.AddLast(parameter);
            }
            Parameters = parameters.ToArray();
        }
    }
}
