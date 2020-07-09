using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class NamespaceDeclaration
    {
        public readonly Token Identifier;
        public readonly Token NamespaceKeyword;
        public readonly Token OpenBrace;
        public readonly ClassDeclaration[] ClassDeclarations;
        public readonly Token CloseBrace;
        public NamespaceDeclaration(LinkedList<Token> tokens)
        {
            Identifier = tokens.GetToken(TokenType.Identifier);
            NamespaceKeyword = tokens.GetToken(TokenType.Keyword, "namespace");
            OpenBrace = tokens.GetToken(TokenType.SyntaxChar, "{");

            LinkedList<ClassDeclaration> classes = new LinkedList<ClassDeclaration>();
            while(!tokens.PopIfMatches(out CloseBrace, TokenType.SyntaxChar, "}"))
            {
                classes.AddLast(new ClassDeclaration(tokens));
            }
            ClassDeclarations = classes.ToArray();
        }
    }
}
