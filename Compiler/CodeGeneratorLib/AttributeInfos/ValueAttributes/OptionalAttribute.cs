using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos
{
    public class OptionalAttribute : ValueAttribute
    {
        public string TypeToPop { get; set; }
        public string NameToPop { get; set; }

        public OptionalAttribute(string[] parts)
            : base(parts[0])
        {
            if (parts.Length != 1) throw new InvalidOperationException();
        }

        public override string GetConstructorParam() => null;
        public override string[] GetBeforeCreationLines() => new string[]
        {
            $"if (tokens.PopIfMatches(out {TypeToPop} {NameToPop}))",
            "{"
        };
        public override string GetValue() => NameToPop;
        public override string[] GetAfterCreationLines() => new string[] { "}" };
    }
}
