using CodeGeneratorLib.AttributeInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneratorLib.SyntaxTreeItemsFieldInfos
{
    class NormalFieldInfo : FieldInfo
    {
        public NormalFieldInfo(string type, string name, AttributeInfo[] attributes)
            : base(type, name, attributes) { }

        public override string[] GetCreationStatements()
        {
            if (ValueAttr == null)
            {
                return new string[]
                {
                    $"{Name} = {NormalInitialization(Type)};"
                };
            }
            else
            {
                LinkedList<string> creationLines = new LinkedList<string>();
                foreach (string before in ValueAttr.GetBeforeCreationLines()) creationLines.AddLast(before);
                string spaces = ValueAttr is OptionalAttribute ? "    " : "";
                creationLines.AddLast(spaces + $"{Name} = {ValueAttr.GetValue()};");
                foreach (string after in ValueAttr.GetAfterCreationLines()) creationLines.AddLast(after);
                return creationLines.ToArray();
            }
        }
    }
}
