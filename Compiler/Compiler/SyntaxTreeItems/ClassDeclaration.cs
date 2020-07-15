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
        public readonly IdentifierToken Name;
        public readonly ModifierList Modifiers;
        public readonly ClassKeywordToken ClassKeyword;
        public readonly OpenCurlyToken OpenCurly;
        public readonly ClassItemDeclaration[] ClassItemDeclarations;
        public readonly CloseCurlyToken CloseCurly;
        public ClassDeclaration(TokenCollection tokens)
        {
            Name = tokens.PopToken<IdentifierToken>();
            Modifiers = new ModifierList(tokens);
            ClassKeyword = tokens.PopToken<ClassKeywordToken>();
            OpenCurly = tokens.PopToken<OpenCurlyToken>();

            LinkedList<ClassItemDeclaration> items = new LinkedList<ClassItemDeclaration>();
            while (!tokens.PopIfMatches(out CloseCurly))
            {
                items.AddLast(ClassItemDeclaration.ReadClassItem(tokens));
            }
            ClassItemDeclarations = items.ToArray();
        }
    }
}
