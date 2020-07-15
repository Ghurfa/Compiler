using CodeGenerator.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.ClassItems
{
    class CheckStatement : ClassItemInfo
    {
        CheckModifier Modifier;
        public ClassItemInfo Inner;
        public CheckStatement(CheckModifier modifier, ClassItemInfo inner)
        {
            Modifier = modifier;
            Inner = inner;
        }
        public override string[] GetDeclaration()
        {
            return Inner.GetDeclaration();
        }

        public override string[] GetCreationStatements()
        {
            LinkedList<string> ret = new LinkedList<string>();
            ret.AddLast($"if ({Modifier.GetCondition()})");
            ret.AddLast("{");
            foreach(string innerLine in Inner.GetCreationStatements())
            {
                ret.AddLast("    " + innerLine);
            }
            ret.AddLast("}");
            return ret.ToArray();
        }
        public override string[] GetToString() => Inner.GetToString();
    }
}
