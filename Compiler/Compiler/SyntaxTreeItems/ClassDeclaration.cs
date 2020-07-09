using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class ClassDeclaration
    {
        public readonly Token Identifier;
        public readonly Token[] AccessModifiers;
        public readonly Token ClassKeyword;
        public readonly Token OpenBrace;
        public readonly ClassItemDeclaration[] ClassItemDeclarations;
        public readonly Token CloseBrace;
        public ClassDeclaration(LinkedList<Token> tokens)
        {
            Identifier = tokens.GetToken(TokenType.Identifier);
            AccessModifiers = tokens.GetModifiers();
            ClassKeyword = tokens.GetToken(TokenType.Keyword, "class");
            OpenBrace = tokens.GetToken(TokenType.SyntaxChar, "{");

            LinkedList<ClassItemDeclaration> items = new LinkedList<ClassItemDeclaration>();
            while (!tokens.PopIfMatches(out CloseBrace, TokenType.SyntaxChar, "}"))
            {
                items.AddLast(ClassItemDeclaration.ReadClassItem(tokens));
            }
            ClassItemDeclarations = items.ToArray();
        }
    }
}
