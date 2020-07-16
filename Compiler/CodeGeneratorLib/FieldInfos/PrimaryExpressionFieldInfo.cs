using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.SyntaxTreeItemsFieldInfos
{
    class PrimaryExpressionFieldInfo : FieldInfo
    {
        public PrimaryExpressionFieldInfo(string type, string name) : base(type, name) { }
        public override string[] GetCreationStatements() =>
            new string[] { $"{Name} = {LowerCaseName} == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : {LowerCaseName};" };
    }
}
