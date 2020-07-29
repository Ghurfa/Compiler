using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class ConstructorDeclaration : ClassItemDeclaration
    {
        public ConstructorKeywordToken ConstructorKeyword { get; private set; }
        public ModifierList Modifiers { get; private set; }
        public ParameterListDeclaration ParameterList { get; private set; }
        public MethodBodyDeclaration Body { get; private set; }

        public ConstructorDeclaration(TokenCollection tokens, ConstructorKeywordToken constructorKeyword)
        {
            ConstructorKeyword = constructorKeyword;
            Modifiers = new ModifierList(tokens);
            ParameterList = new ParameterListDeclaration(tokens);
            Body = new MethodBodyDeclaration(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += ConstructorKeyword.ToString();
            ret += Modifiers.ToString();
            ret += ParameterList.ToString();
            ret += Body.ToString();
            return ret;
        }
    }
}
