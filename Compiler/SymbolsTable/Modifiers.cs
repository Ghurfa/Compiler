using Parser.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using Tokenizer;

namespace SymbolsTable
{
    public enum AccessModifier
    {
        PublicModifier,
        PrivateModifier,
        ProtectedModifier,
    }

    public class Modifiers
    {
        public AccessModifier AccessModifier { get; set; }
        public bool IsStatic { get; set; }

        public Modifiers(AccessModifier accessModifier, bool isStatic)
        {
            AccessModifier = accessModifier;
            IsStatic = isStatic;
        }

        public Modifiers(ModifierList modifiers)
        {
            if (modifiers == null)
            {
                AccessModifier = AccessModifier.PrivateModifier;
                IsStatic = false;
            }
            else
            {
                IsStatic = false;
                foreach (ModifierToken modifier in modifiers.Modifiers)
                {
                    switch (modifier.Text)
                    {
                        case "public": AccessModifier = AccessModifier.PublicModifier; break;
                        case "private": AccessModifier = AccessModifier.PrivateModifier; break;
                        case "protected": AccessModifier = AccessModifier.ProtectedModifier; break;
                        case "static": IsStatic = true; break;
                        default: throw new NotImplementedException();
                    }
                }
            }
        }

        public static Modifiers Public => new Modifiers(AccessModifier.PublicModifier, false);
    }
}
