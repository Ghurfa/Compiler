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
        public readonly Token OpenPeren;
        public readonly ParameterDeclaration[] Parameters;
        public readonly Token ClosePeren;
        public ParameterListDeclaration(TokenCollection tokens)
        {
            OpenPeren = tokens.PopToken(TokenType.OpenPeren);
            LinkedList<ParameterDeclaration> parameters = new LinkedList<ParameterDeclaration>();
            bool lastMissingComma = false;
            while (!tokens.PopIfMatches(out ClosePeren, TokenType.ClosePeren))
            {
                if (lastMissingComma) throw new UnexpectedToken(tokens.PeekToken());
                var parameter = new ParameterDeclaration(tokens);
                lastMissingComma = parameter.CommaToken == null;
                parameters.AddLast(parameter);
            }
            Parameters = parameters.ToArray();
        }
    }
}
