using System;
using System.Collections.Generic;

namespace Compiler.SyntaxTreeItems
{
    public class MethodDeclaration : ClassItemDeclaration
    {
        public readonly Token Identifier;
        public readonly Token[] AccessModifiers;
        public readonly Type ReturnType;
        public readonly ParameterListDeclaration ParameterList;
        public readonly MethodBodyDeclaration MethodBody;
        public MethodDeclaration(TokenCollection tokens, Token identifierToken)
        {
            Identifier = identifierToken;
            AccessModifiers = tokens.ReadModifiers();
            ReturnType = Type.ReadType(tokens);
            ParameterList = new ParameterListDeclaration(tokens);
            MethodBody = new MethodBodyDeclaration(tokens);
        }
    }
}
