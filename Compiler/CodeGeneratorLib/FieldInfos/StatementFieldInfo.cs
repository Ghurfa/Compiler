using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.ClassItems
{
    class StatementFieldInfo : FieldInfo
    {
        public StatementFieldInfo(string type, string name, AttributeInfo[] attributes)
            : base(type, name, attributes) { }

        public override string[] GetCreationStatements() =>
            new string[] { $"{Name} = {LowerCaseName} == null ? Statement.ReadStatement(tokens) : {LowerCaseName};" };
    }
}
