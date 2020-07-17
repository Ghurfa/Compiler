using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneratorLib.AttributeInfos.ConditionAttributes
{
    public class NotMissingCommaAttribute : ConditionAttribute
    {
        public NotMissingCommaAttribute(string[] parts)
                    : base(parts[0])
        {
            if (parts.Length != 1) throw new InvalidOperationException();
        }

        public override string[] GetInitializingStatements() => new string[]
            {
                "bool lastMissingComma = tokens.PeekToken() is ClosePerenToken;"
            };

        public override string GetCondition() => "!lastMissingComma";
        public override string[] GetUpdateStatements() => new string[]
        {
            "lastMissingComma = add.Comma == null;"
        };
    }
}
