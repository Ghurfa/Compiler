using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MethodDeclaration : ClassItemDeclaration
    {
        public IdentifierToken Name { get; private set; }
        public ModifierList Modifiers { get; private set; }
        public Type ReturnType { get; private set; }
        public ParameterListDeclaration ParameterList { get; private set; }
        public MethodBodyDeclaration Body { get; private set; }

        public MethodDeclaration(TokenCollection tokens, IdentifierToken name)
        {
            Name = name;
            Modifiers = new ModifierList(tokens);
            ReturnType = Type.ReadType(tokens);
            ParameterList = new ParameterListDeclaration(tokens);
            Body = new MethodBodyDeclaration(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Name.ToString();
            ret += Modifiers.ToString();
            ret += ReturnType.ToString();
            ret += ParameterList.ToString();
            ret += Body.ToString();
            return ret;
        }
    }
}
