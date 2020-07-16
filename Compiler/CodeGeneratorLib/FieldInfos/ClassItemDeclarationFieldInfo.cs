using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.ClassItems
{
    class ClassItemDeclarationFieldInfo : FieldInfo
    {
        public ClassItemDeclarationFieldInfo(string type, string name) : base(type, name) { }
        public override string[] GetCreationStatements() =>
            new string[] { $"{Name} = {LowerCaseName} == null ? ClassItemDeclaration.ReadClassItem(tokens) : {LowerCaseName};" };
    }
}
