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
                if (lastMissingComma) throw new SyntaxTreeBuildingException(tokens.PeekToken());
                var parameter = new ParameterDeclaration(tokens);
                lastMissingComma = parameter.CommaToken == null;
                parameters.AddLast(parameter);
            }
            Parameters = parameters.ToArray();
        }
    }
    public class ParameterDeclaration
    {
        public readonly Token Identifier;
        public readonly Token ColonToken;
        public readonly Type TypeParameter;
        public readonly Token? CommaToken;
        public ParameterDeclaration(TokenCollection tokens)
        {
            Identifier = tokens.PopToken(TokenType.Identifier);
            ColonToken = tokens.PopToken(TokenType.Colon);
            TypeParameter = Type.ReadType(tokens);
            if (tokens.PopIfMatches(out Token comma, TokenType.Comma))
            {
                CommaToken = comma;
            }
        }
    }
}
