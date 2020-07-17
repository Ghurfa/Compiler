using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.SyntaxTreeItemsFieldInfos
{
    class UnaryExpressionFieldInfo : FieldInfo
    {
        public UnaryExpressionFieldInfo(string type, string name, AttributeInfo[] attributes)
            : base(type, name, attributes) { }

        public override string[] GetCreationStatements() =>
            new string[] { $"{Name} = {LowerCaseName} == null ? UnaryExpression.ReadUnaryExpression(tokens) : {LowerCaseName};" };
    }
}
