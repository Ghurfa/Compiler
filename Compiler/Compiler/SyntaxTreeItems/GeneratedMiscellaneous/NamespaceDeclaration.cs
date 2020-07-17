using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class NamespaceDeclaration
    {
        public QualifiedIdentifier Name { get; private set; }
        public NamespaceKeywordToken NamespaceKeyword { get; private set; }
        public OpenCurlyToken OpenCurly { get; private set; }
        public ClassDeclaration[] ClassDeclarations { get; private set; }
        private CloseCurlyToken closeCurly;
        public CloseCurlyToken CloseCurly { get => closeCurly; private set { closeCurly = value; } }

        public NamespaceDeclaration(TokenCollection tokens)
        {
            Name = new QualifiedIdentifier(tokens);
            NamespaceKeyword = tokens.PopToken<NamespaceKeywordToken>();
            OpenCurly = tokens.PopToken<OpenCurlyToken>();
            LinkedList<ClassDeclaration> classDeclarationsList = new LinkedList<ClassDeclaration>();
            while (!tokens.PopIfMatches(out this.closeCurly))
            {
                var add = new ClassDeclaration(tokens);
                classDeclarationsList.AddLast(add);
            }
            ClassDeclarations = classDeclarationsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            ret += Name.ToString();
            ret += " ";
            ret += NamespaceKeyword.ToString();
            ret += " ";
            ret += OpenCurly.ToString();
            ret += " ";
            foreach (var item in ClassDeclarations)
            {
                ret += item.ToString();
            }
            ret += " ";
            ret += CloseCurly.ToString();
            return ret;
        }
    }
}
