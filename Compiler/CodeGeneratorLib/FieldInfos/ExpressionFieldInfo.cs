using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.SyntaxTreeItemsFieldInfos
{
    class ExpressionFieldInfo : FieldInfo
    {
        public ExpressionFieldInfo(string type, string name) : base(type, name) { }
        public override string[] GetCreationStatements() =>
            new string[] { $"{Name} = {LowerCaseName} == null ? Expression.ReadExpression(tokens) : {LowerCaseName};" };
    }
}
