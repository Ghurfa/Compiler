using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos
{
    public class NotPopAttribute : ConditionAttribute
    {
        public string FieldToPop { get; set; }

        public NotPopAttribute(string[] parts)
            : base(parts[0])
        {
            if (parts.Length != 2) throw new InvalidOperationException();
            FieldToPop = parts[1];
        }

        public override string[] GetInitializingStatements() => new string[0];
        public override string GetCondition() => $"!tokens.PopIfMatches(out {FieldToPop})";
        public override string[] GetUpdateStatements() => new string[0];
    }
}
