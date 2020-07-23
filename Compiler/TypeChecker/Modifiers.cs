using Compiler;
using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker
{
    enum AccessModifier
    {
        PublicModifier,
        PrivateModifier,
        ProtectedModifier,
    }

    class Modifiers
    {
        public AccessModifier AccessModifier { get; set; }

        public Modifiers(ModifierList modifiers)
        {
            if (modifiers == null) return;

            foreach(ModifierToken modifier in modifiers.Modifiers)
            {
                switch(modifier.Text)
                {
                    case "public": AccessModifier = AccessModifier.PublicModifier; break;
                    case "private": AccessModifier = AccessModifier.PrivateModifier; break;
                    case "protected": AccessModifier = AccessModifier.ProtectedModifier; break;
                    default: throw new NotImplementedException();
                }
            }
        }
    }
}
