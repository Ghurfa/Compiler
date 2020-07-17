using Compiler.SyntaxTreeItems;
using Compiler.SyntaxTreeItems.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class NamespaceDeclaration
    {
        public QualifiedIdentifier Name;
        public NamespaceKeywordToken NamespaceKeyword;
        public OpenCurlyToken OpenCurly;
        public ClassDeclaration[] ClassDeclarations;
        public CloseCurlyToken CloseCurly;

        public NamespaceDeclaration(TokenCollection tokens)
        {
            Name = new QualifiedIdentifier(tokens);
            NamespaceKeyword = tokens.PopToken<NamespaceKeywordToken>();
            OpenCurly = tokens.PopToken<OpenCurlyToken>();

            LinkedList<ClassDeclaration> classes = new LinkedList<ClassDeclaration>();
            while (!tokens.PopIfMatches(out CloseCurly))
            {
                classes.AddLast(new ClassDeclaration(tokens));
            }
            ClassDeclarations = classes.ToArray();
        }
    }
}
