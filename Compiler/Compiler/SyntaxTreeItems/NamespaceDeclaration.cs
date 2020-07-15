using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class NamespaceDeclaration
    {
        public readonly QualifiedIdentifier Name;
        public readonly NamespaceKeywordToken NamespaceKeyword;
        public readonly OpenCurlyToken OpenCurly;
        public readonly ClassDeclaration[] ClassDeclarations;
        public readonly CloseCurlyToken CloseCurly;

        public  IToken LeftToken => Name.LeftToken;
        public  IToken RightToken => CloseCurly;

        public NamespaceDeclaration(TokenCollection tokens, QualifiedIdentifier name = null, NamespaceKeywordToken? namespaceKeyword = null, OpenCurlyToken? openCurly = null, ClassDeclaration[] classDeclarations = null)
        {
            Name = name == null ? new QualifiedIdentifier(tokens) : name;
            NamespaceKeyword = namespaceKeyword == null ? tokens.PopToken<NamespaceKeywordToken>() : (NamespaceKeywordToken)namespaceKeyword;
            OpenCurly = openCurly == null ? tokens.PopToken<OpenCurlyToken>() : (OpenCurlyToken)openCurly;
            var classDeclarationsList = new LinkedList<ClassDeclaration>();
            if (classDeclarations != null)
            {
                foreach (var item in classDeclarations)
                {
                    classDeclarationsList.AddLast(item);
                }
            }
            while(!tokens.PopIfMatches(out CloseCurly))
            {
                var newItem = new ClassDeclaration(tokens);
                classDeclarationsList.AddLast(newItem);
            }
            ClassDeclarations = classDeclarationsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
