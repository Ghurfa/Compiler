using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ConstructorDeclaration : ClassItemDeclaration
    {
        public readonly Token CtorKeyword;
        public readonly Token[] AccessModifiers;
        public readonly MethodParamsListDeclaration ParameterList;
        public readonly MethodBodyDeclaration ConstructorBody;
        public ConstructorDeclaration(LinkedList<Token> tokens, Token ctorKeywordToken)
        {
            CtorKeyword = ctorKeywordToken;
            AccessModifiers = tokens.GetModifiers();
            ParameterList = new MethodParamsListDeclaration(tokens);
            ConstructorBody = new MethodBodyDeclaration(tokens);
        }
    }
}
