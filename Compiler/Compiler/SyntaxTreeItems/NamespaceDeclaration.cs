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
        public IToken NamespaceKeyword;
        public IToken OpenBrace;
        public ClassDeclaration[] ClassDeclarations;
        public IToken CloseBrace;
        public NamespaceDeclaration(TokenCollection tokens)
        {
            Name = new QualifiedIdentifier(tokens);
            NamespaceKeyword = tokens.PopToken(TokenType.NamespaceKeyword);
            OpenBrace = tokens.PopToken(TokenType.OpenCurly);

            LinkedList<ClassDeclaration> classes = new LinkedList<ClassDeclaration>();
            while (!tokens.PopIfMatches(out CloseBrace, TokenType.CloseCurly))
            {
                classes.AddLast(new ClassDeclaration(tokens));
            }
            ClassDeclarations = classes.ToArray();
        }
    }
}
