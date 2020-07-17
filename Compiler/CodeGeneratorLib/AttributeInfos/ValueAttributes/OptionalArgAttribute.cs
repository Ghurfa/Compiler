using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos.ValueAttributes
{
    class OptionalArgAttribute : ValueAttribute
    {
        public string Type { get; set; }
        public string ArgumentName { get; set; }
        public string NormalInitialization { get; set; }
        public OptionalArgAttribute(string[] parts)
            : base(parts[0])
        {
            if (parts.Length != 1) throw new InvalidOperationException();
        }

        public override string GetConstructorParam() => $"{Type} {ArgumentName} = null";
        public override string[] GetAfterCreationLines() => new string[0];
        public override string GetValue() => $"{ArgumentName} ?? {NormalInitialization}";
        public override string[] GetBeforeCreationLines() => new string[0];
    }
}
