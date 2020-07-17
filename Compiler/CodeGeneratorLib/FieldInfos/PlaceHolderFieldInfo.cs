using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.SyntaxTreeItemsFieldInfos
{
    class PlaceHolderFieldInfo : FieldInfo
    {
        public PlaceHolderFieldInfo(string type, string name, AttributeInfo[] attributes)
            : base(type, name, attributes) { }

        public override string[] GetCreationStatements() => throw new InvalidOperationException();
    }
}
