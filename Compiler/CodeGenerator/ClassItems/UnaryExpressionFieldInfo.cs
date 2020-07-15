using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.SyntaxTreeItemsFieldInfos
{
    class UnaryExpressionFieldInfo : FieldInfo
    {
        public UnaryExpressionFieldInfo(string type, string name) : base(type, name) { }
        public override string[] GetCreationStatements() =>
            new string[] { $"{Name} = {LowerCaseName} == null ? UnaryExpression.ReadUnaryExpression(tokens) : {LowerCaseName};" };
    }
}
