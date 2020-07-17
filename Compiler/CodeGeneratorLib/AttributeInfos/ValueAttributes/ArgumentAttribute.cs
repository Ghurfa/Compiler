using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos
{
    public class ArgumentAttribute : ValueAttribute
    {
        public string Type { get; set; }
        public string ArgumentName { get; set; }
        public ArgumentAttribute(string[] parts)
            : base(parts[0])
        {
            if (parts.Length != 1) throw new InvalidOperationException();
        }

        public override string GetConstructorParam() => $"{Type} {ArgumentName}";
        public override string[] GetAfterCreationLines() => new string[0];
        public override string GetValue() => ArgumentName;
        public override string[] GetBeforeCreationLines() => new string[0];
    }
}
