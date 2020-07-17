using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ConstructorDeclaration : ClassItemDeclaration
    {
        public ConstructorKeywordToken ConstructorKeyword { get; private set; }
        public ModifierList Modifiers { get; private set; }
        public ParameterListDeclaration ParameterList { get; private set; }
        public MethodBodyDeclaration ConstructorBody { get; private set; }

        public ConstructorDeclaration(TokenCollection tokens, ConstructorKeywordToken constructorKeyword)
        {
            ConstructorKeyword = constructorKeyword;
            Modifiers = new ModifierList(tokens);
            ParameterList = new ParameterListDeclaration(tokens);
            ConstructorBody = new MethodBodyDeclaration(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += ConstructorKeyword.ToString();
            ret += " ";
            ret += Modifiers.ToString();
            ret += " ";
            ret += ParameterList.ToString();
            ret += " ";
            ret += ConstructorBody.ToString();
            return ret;
        }
    }
}
