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
        public MethodDeclaration(LinkedList<Token> tokens, Token identifierToken, Token[] modifierTokens, TypeToken returnTypeToken, Token openPerenToken)
        {
            Identifier = identifierToken;
            AccessModifiers = modifierTokens;
            ReturnType = returnTypeToken;
            ParameterList = new MethodParamsListDeclaration(tokens, openPerenToken);
            MethodBody = new MethodBodyDeclaration(tokens);
        }
    }
}
