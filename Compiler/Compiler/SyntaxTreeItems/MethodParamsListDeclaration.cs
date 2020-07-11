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
        public readonly Token OpenPerenthesesToken;
        public readonly ParameterDeclaration[] Parameters;
        public readonly Token ClosePerenthesesToken;
        public ParameterListDeclaration(TokenCollection tokens)
        {
            OpenPerenthesesToken = tokens.PopToken(TokenType.OpenPeren);
            LinkedList<ParameterDeclaration> parameters = new LinkedList<ParameterDeclaration>();
            bool lastMissingComma = false;
            while(!tokens.PopIfMatches(out ClosePerenthesesToken, TokenType.ClosePeren))
            {
                if (lastMissingComma) throw new SyntaxTreeBuildingException(tokens.PeekToken());
                var parameter = new ParameterDeclaration(tokens);
                lastMissingComma = parameter.CommaToken == null;
                parameters.AddLast(parameter);
            }
            Parameters = parameters.ToArray();
        }
    }
}
