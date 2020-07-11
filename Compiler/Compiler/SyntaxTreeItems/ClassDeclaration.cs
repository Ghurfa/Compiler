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
        public ClassDeclaration(TokenCollection tokens)
        {
            Identifier = tokens.PopToken(TokenType.Identifier);
            AccessModifiers = tokens.ReadModifiers();
            ClassKeyword = tokens.PopToken(TokenType.ClassKeyword);
            OpenBrace = tokens.PopToken(TokenType.OpenCurly);

            LinkedList<ClassItemDeclaration> items = new LinkedList<ClassItemDeclaration>();
            while (!tokens.PopIfMatches(out CloseBrace, TokenType.CloseCurly))
            {
                items.AddLast(ClassItemDeclaration.ReadClassItem(tokens));
            }
            ClassItemDeclarations = items.ToArray();
        }
    }
}
