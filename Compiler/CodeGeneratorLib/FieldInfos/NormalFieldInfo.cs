using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.SyntaxTreeItemsFieldInfos
{
    class NormalFieldInfo : FieldInfo
    {
        public NormalFieldInfo(string type, string name, AttributeInfo[] attributes)
            : base(type, name, attributes) { }

        public override string[] GetCreationStatements() =>
            new string[] { $"{Name} = {LowerCaseName} == null ? new {Type}(tokens) : {LowerCaseName};" };
    }
}
