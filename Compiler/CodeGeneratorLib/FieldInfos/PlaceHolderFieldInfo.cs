using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.SyntaxTreeItemsFieldInfos
{
    class PlaceHolderFieldInfo : FieldInfo
    {
        public PlaceHolderFieldInfo(string type, string name) : base(type, name) { }
        public override string[] GetCreationStatements() => throw new InvalidOperationException();
    }
}
