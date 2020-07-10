using System;
using System.Collections.Generic;

namespace Compiler.SyntaxTreeItems
{
    public class MethodDeclaration : ClassItemDeclaration
    {
        public readonly Token Identifier;
        public readonly Token[] AccessModifiers;
        public readonly TypeToken ReturnType;
        public readonly MethodParamsListDeclaration ParameterList;
        public readonly MethodBodyDeclaration MethodBody;
        public MethodDeclaration(LinkedList<Token> tokens, Token identifierToken)
        {
            Identifier = identifierToken;
            AccessModifiers = tokens.GetModifiers();
            ReturnType = new TypeToken(tokens);
            ParameterList = new MethodParamsListDeclaration(tokens);
            MethodBody = new MethodBodyDeclaration(tokens);
        }
    }
}
