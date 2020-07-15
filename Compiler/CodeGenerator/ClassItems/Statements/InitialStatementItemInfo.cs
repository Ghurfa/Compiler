using CodeGenerator.ClassItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.ClassItems
{
    abstract class InitialStatementItemInfo : ClassItemInfo
    {
        public override string[] GetDeclaration() => new string[0];
        public override string[] GetToString() => new string[0];
        public static InitialStatementItemInfo ReadStatementItem(string text)
        {
            string[] parts = text.Split(' ');
            switch(parts[0].ToLower())
            {
                case "ensurewhitespaceafter": return new EnsureWhitespaceAfterItem(parts.ExceptFirst());
                case "ensurelinebreakafter": return new EnsureLineBreakAfterItem(parts.ExceptFirst());
                case "throw": return new ThrowItemInfo(parts.ExceptFirst());
                default: throw new InvalidOperationException();
            }
        }
    }
}
