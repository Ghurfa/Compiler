using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class MethodDeclaration : ClassItemDeclaration
    {
        public readonly IdentifierToken Name;
        public readonly ModifierList Modifiers;
        public readonly Type ReturnType;
        public readonly ParameterListDeclaration ParameterList;
        public readonly MethodBodyDeclaration Body;

        public override IToken LeftToken => Name;
        public override IToken RightToken => Body.RightToken;

        public MethodDeclaration(TokenCollection tokens, IdentifierToken? name = null, ModifierList modifiers = null, Type returnType = null, ParameterListDeclaration parameterList = null, MethodBodyDeclaration body = null)
        {
            Name = name == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)name;
            Modifiers = modifiers == null ? new ModifierList(tokens) : modifiers;
            ReturnType = returnType == null ? Type.ReadType(tokens) : returnType;
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
