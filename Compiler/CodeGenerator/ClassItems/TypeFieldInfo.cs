using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.SyntaxTreeItemsFieldInfos
{
    class TypeFieldInfo : FieldInfo
    {
        public TypeFieldInfo(string type, string name) : base(type, name) { }
        public override string[] GetCreationStatements() =>
            new string[] { $"{Name} = {LowerCaseName} == null ? Type.ReadType(tokens) : {LowerCaseName};" };
    }
}
