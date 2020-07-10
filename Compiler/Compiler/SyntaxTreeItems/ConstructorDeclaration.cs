using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ConstructorDeclaration : ClassItemDeclaration
    {
        public readonly Token CtorKeyword;
        public readonly Token[] AccessModifiers;
        public readonly ParameterListDeclaration ParameterList;
        public readonly MethodBodyDeclaration ConstructorBody;
        public ConstructorDeclaration(TokenCollection tokens, Token ctorKeywordToken)
        {
            CtorKeyword = ctorKeywordToken;
            AccessModifiers = tokens.ReadModifiers();
            ParameterList = new ParameterListDeclaration(tokens);
            ConstructorBody = new MethodBodyDeclaration(tokens);
        }
    }
}
