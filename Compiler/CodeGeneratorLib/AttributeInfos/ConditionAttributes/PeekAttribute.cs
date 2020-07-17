using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos.ConditionAttributes
{
    public class PeekAttribute : ConditionAttribute
    {
        public string TypeToPeek { get; set; }
        public PeekAttribute(string[] parts) : base(parts[0])
        {
            if (parts.Length != 2) throw new InvalidOperationException();
            TypeToPeek = parts[1].EndsWith("Token") ? parts[1] : parts[1] + "Token";
        }
        public override string GetCondition() => $"tokens.PeekToken() is {TypeToPeek}";

        public override string[] GetInitializingStatements() => new string[0];

        public override string[] GetUpdateStatements() => new string[0];
    }
}
