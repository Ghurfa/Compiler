using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Modifiers
{
    public abstract class Modifier
    {
        public static Modifier ReadModifier(string text)
        {
            string[] parts = text.Split(' ');
            string keyword = parts[0];
            switch (keyword.ToLower())
            {
                case "ensurevalidstatementending": return new EnsureValidStatementEndingModifier();
                case "ifnotnull": return new IfNotNullModifier(parts.ExceptFirst());
                case "ifnottype": return new IfNotTypeModifier(parts.ExceptFirst());
                case "untilpop": return new UntilPopModifier(parts.ExceptFirst());
                case "whilepoptype": return new WhilePopTypeModifier(parts.ExceptFirst());
                case "until": return new UntilModifier(parts.ExceptFirst());
                default: throw new InvalidOperationException();
            }
        }
    }
}
