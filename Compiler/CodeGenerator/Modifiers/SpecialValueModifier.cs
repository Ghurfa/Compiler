using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Modifiers
{
    abstract class SpecialValueModifier : InitialModifier
    {
        public abstract string GetSpecialValueExpression();
    }
}
