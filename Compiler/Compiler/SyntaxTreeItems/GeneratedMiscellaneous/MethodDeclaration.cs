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
        public MethodBodyDeclaration MethodBody { get; private set; }

        public MethodDeclaration(TokenCollection tokens, IdentifierToken name)
        {
            Name = name;
            Modifiers = new ModifierList(tokens);
            ReturnType = Type.ReadType(tokens);
            ParameterList = new ParameterListDeclaration(tokens);
            MethodBody = new MethodBodyDeclaration(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Name.ToString();
            ret += " ";
            ret += Modifiers.ToString();
            ret += " ";
            ret += ReturnType.ToString();
            ret += " ";
            ret += ParameterList.ToString();
            ret += " ";
            ret += MethodBody.ToString();
            return ret;
        }
    }
}
