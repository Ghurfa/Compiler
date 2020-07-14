using System;
using System.Collections.Generic;

namespace Compiler.SyntaxTreeItems
{
    public class MethodDeclaration : ClassItemDeclaration
    {
        public readonly IToken Name;
        public readonly ModifierList Modifiers;
        public readonly Type ReturnType;
        public readonly ParameterListDeclaration ParameterList;
        public readonly MethodBodyDeclaration MethodBody;
        public MethodDeclaration(TokenCollection tokens, IToken identifierToken)
        {
            Name = identifierToken;
            Modifiers = new ModifierList(tokens);
            ReturnType = Type.ReadType(tokens);
            ParameterList = new ParameterListDeclaration(tokens);
            MethodBody = new MethodBodyDeclaration(tokens);
        }
    }
}
