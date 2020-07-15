using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ClassDeclaration
    {
        public readonly IdentifierToken Name;
        public readonly ModifierList Modifiers;
        public readonly ClassKeywordToken ClassKeyword;
        public readonly OpenCurlyToken OpenCurly;
        public readonly ClassItemDeclaration[] ItemDeclarations;
        public readonly CloseCurlyToken CloseCurly;

        public  IToken LeftToken => Name;
        public  IToken RightToken => CloseCurly;

        public ClassDeclaration(TokenCollection tokens, IdentifierToken? name = null, ModifierList modifiers = null, ClassKeywordToken? classKeyword = null, OpenCurlyToken? openCurly = null, ClassItemDeclaration[] itemDeclarations = null)
        {
            Name = name == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)name;
            Modifiers = modifiers == null ? new ModifierList(tokens) : modifiers;
            ClassKeyword = classKeyword == null ? tokens.PopToken<ClassKeywordToken>() : (ClassKeywordToken)classKeyword;
            OpenCurly = openCurly == null ? tokens.PopToken<OpenCurlyToken>() : (OpenCurlyToken)openCurly;
            var itemDeclarationsList = new LinkedList<ClassItemDeclaration>();
            if (itemDeclarations != null)
            {
                foreach (var item in itemDeclarations)
                {
                    itemDeclarationsList.AddLast(item);
                }
            }
            while(!tokens.PopIfMatches(out CloseCurly))
            {
                var newItem = ClassItemDeclaration.ReadClassItem(tokens);
                itemDeclarationsList.AddLast(newItem);
            }
            ItemDeclarations = itemDeclarationsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            
            return ret;
        }
    }
}
