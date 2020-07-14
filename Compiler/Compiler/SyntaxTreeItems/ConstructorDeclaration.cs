using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ConstructorDeclaration : ClassItemDeclaration
    {
        public readonly IToken CtorKeyword;
        public readonly ModifierList Modifiers;
        public readonly ParameterListDeclaration ParameterList;
        public readonly MethodBodyDeclaration ConstructorBody;
        public ConstructorDeclaration(TokenCollection tokens, IToken ctorKeywordToken)
        {
            CtorKeyword = ctorKeywordToken;
            Modifiers = new ModifierList(tokens);
            ParameterList = new ParameterListDeclaration(tokens);
            ConstructorBody = new MethodBodyDeclaration(tokens);
        }
    }
}
