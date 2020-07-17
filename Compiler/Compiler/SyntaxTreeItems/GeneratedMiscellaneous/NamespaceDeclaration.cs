using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

        public NamespaceDeclaration(TokenCollection tokens, QualifiedIdentifier name = null, NamespaceKeywordToken? namespaceKeyword = null, OpenCurlyToken? openCurly = null, ClassDeclaration[] classDeclarations = null, CloseCurlyToken? closeCurly = null)
        {
            Name = name == null ? new QualifiedIdentifier(tokens) : name;
            NamespaceKeyword = namespaceKeyword == null ? tokens.PopToken<NamespaceKeywordToken>() : (NamespaceKeywordToken)namespaceKeyword;
            OpenCurly = openCurly == null ? tokens.PopToken<OpenCurlyToken>() : (OpenCurlyToken)openCurly;
            LinkedList<ClassDeclaration> classDeclarationsList = new LinkedList<ClassDeclaration>();
            while (!tokens.PopIfMatches(out this.closeCurly))
            {
                classDeclarationsList.AddLast(new ClassDeclaration(tokens));
            }
            ClassDeclarations = classDeclarationsList.ToArray();
        }
    }
}
