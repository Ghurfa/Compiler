using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos.ValueAttributes
{
    class ValidStatementEndingAttribute : ValueAttribute
    {
        public ValidStatementEndingAttribute(string[] parts)
            : base(parts[0])
        {
            if (parts.Length != 1) throw new InvalidOperationException();
        }

        public override string GetConstructorParam() => null;
        public override string[] GetBeforeCreationLines() => new string[0];
        public override string GetValue() => "tokens.EnsureValidStatementEnding()";
        public override string[] GetAfterCreationLines() => new string[0];
    }
}
