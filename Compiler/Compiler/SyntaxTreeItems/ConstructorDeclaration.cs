using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ConstructorDeclaration : ClassItemDeclaration
    {
        public readonly ConstructorKeywordToken ConstructorKeyword;
        public readonly ModifierList Modifiers;
        public readonly ParameterListDeclaration ParameterList;
        public readonly MethodBodyDeclaration Body;

        public override IToken LeftToken => ConstructorKeyword;
        public override IToken RightToken => Body.RightToken;

        public ConstructorDeclaration(TokenCollection tokens, ConstructorKeywordToken? constructorKeyword = null, ModifierList modifiers = null, ParameterListDeclaration parameterList = null, MethodBodyDeclaration body = null)
        {
            ConstructorKeyword = constructorKeyword == null ? tokens.PopToken<ConstructorKeywordToken>() : (ConstructorKeywordToken)constructorKeyword;
            Modifiers = modifiers == null ? new ModifierList(tokens) : modifiers;
            ParameterList = parameterList == null ? new ParameterListDeclaration(tokens) : parameterList;
            Body = body == null ? new MethodBodyDeclaration(tokens) : body;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            
            return ret;
        }
    }
}
