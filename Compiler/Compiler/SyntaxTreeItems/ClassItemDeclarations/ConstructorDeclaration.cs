using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ConstructorDeclaration : ClassItemDeclaration
    {
        public readonly ConstructorKeywordToken ConstructorKeyword;
        public readonly ModifierList Modifiers;
        public readonly ParameterListDeclaration ParameterList;
        public readonly MethodBodyDeclaration ConstructorBody;
        public ConstructorDeclaration(TokenCollection tokens, ConstructorKeywordToken constructorKeyword)
        {
            ConstructorKeyword = constructorKeyword;
            Modifiers = new ModifierList(tokens);
            ParameterList = new ParameterListDeclaration(tokens);
            ConstructorBody = new MethodBodyDeclaration(tokens);
        }
    }
}
