using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.SyntaxTreeItemsFieldInfos
{
    class NormalFieldInfo : FieldInfo
    {
        public NormalFieldInfo(string type, string name) : base(type, name) { }
        public override string[] GetCreationStatements() =>
            new string[] { $"{Name} = {LowerCaseName} == null ? new {Type}(tokens) : {LowerCaseName};" };
    }
}
