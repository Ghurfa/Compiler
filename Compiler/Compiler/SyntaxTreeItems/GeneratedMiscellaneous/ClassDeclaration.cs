using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ClassDeclaration
    {
        public IdentifierToken Name { get; private set; }
        public ModifierList Modifiers { get; private set; }
        public ClassKeywordToken ClassKeyword { get; private set; }
        public OpenCurlyToken OpenCurly { get; private set; }
        public ClassItemDeclaration[] ClassItems { get; private set; }
        private CloseCurlyToken closeCurly;
        public CloseCurlyToken CloseCurly { get => closeCurly; private set { closeCurly = value; } }

        public ClassDeclaration(TokenCollection tokens)
        {
            Name = tokens.PopToken<IdentifierToken>();
            Modifiers = new ModifierList(tokens);
            ClassKeyword = tokens.PopToken<ClassKeywordToken>();
            OpenCurly = tokens.PopToken<OpenCurlyToken>();
            LinkedList<ClassItemDeclaration> classItemsList = new LinkedList<ClassItemDeclaration>();
            while (!tokens.PopIfMatches(out this.closeCurly))
            {
                var add = ClassItemDeclaration.ReadClassItem(tokens);
                classItemsList.AddLast(add);
            }
            ClassItems = classItemsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            ret += Name.ToString();
            ret += " ";
            ret += Modifiers.ToString();
            ret += " ";
            ret += ClassKeyword.ToString();
            ret += " ";
            ret += OpenCurly.ToString();
            ret += " ";
            foreach (var item in ClassItems)
            {
                ret += item.ToString();
            }
            ret += " ";
            ret += CloseCurly.ToString();
            return ret;
        }
    }
}
