using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.SyntaxTreeItemsFieldInfos
{
    class TokenFieldInfo : FieldInfo
    {
        public TokenFieldInfo(string type, string name, AttributeInfo[] attributes)
            : base(type, name, attributes) { }

        public override string[] GetCreationStatements() => new string[]{
            $"{Name} = {LowerCaseName} == null ? tokens.PopToken<{Type}>() : ({Type}){LowerCaseName};" };
    }
}
