using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace Compiler.SyntaxTreeItems
{
    public class MethodParamsListDeclaration
    {
        public readonly Token OpenPerenthesesToken;
        public readonly ParameterDeclaration[] Parameters;
        public readonly Token ClosePerenthesesToken;
        public MethodParamsListDeclaration(LinkedList<Token> tokens)
            : this(tokens, tokens.GetToken(TokenType.SyntaxChar, "("))
        {

        }
        public MethodParamsListDeclaration(LinkedList<Token> tokens, Token openPerenToken)
        {
            OpenPerenthesesToken = openPerenToken;
            LinkedList<ParameterDeclaration> parameters = new LinkedList<ParameterDeclaration>();
            bool lastMissingComma = false;
            while(!tokens.PopIfMatches(out ClosePerenthesesToken, TokenType.SyntaxChar, ")"))
            {
                if (lastMissingComma) throw new SyntaxTreeBuildingException(tokens.First.Value);
                var parameter = new ParameterDeclaration(tokens);
                lastMissingComma = parameter.CommaToken == null;
                parameters.AddLast(parameter);
            }
            Parameters = parameters.ToArray();
        }
    }
}
