using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.SyntaxTreeItemsFieldInfos
{
    class TokenFieldInfo : FieldInfo
    {
        public TokenFieldInfo(string type, string name) : base(type, name) { }
        public override string[] GetCreationStatements() => new string[]{
            $"{Name} = {LowerCaseName} == null ? tokens.PopToken<{Type}>() : ({Type}){LowerCaseName};" };
    }
}
