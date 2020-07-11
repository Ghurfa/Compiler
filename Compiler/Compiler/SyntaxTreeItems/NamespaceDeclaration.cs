using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class NamespaceDeclaration
    {
        public Token Identifier;
        public Token NamespaceKeyword;
        public Token OpenBrace;
        public ClassDeclaration[] ClassDeclarations;
        public Token CloseBrace;
        public NamespaceDeclaration(TokenCollection tokens)
        {
            Identifier = tokens.PopToken(TokenType.Identifier);
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
