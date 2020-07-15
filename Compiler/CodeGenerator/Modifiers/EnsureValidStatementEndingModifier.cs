using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Modifiers
{
    class EnsureValidStatementEndingModifier : SpecialValueModifier
    {
        public EnsureValidStatementEndingModifier()
        {
        }
        public override string GetSpecialValueExpression() => $"tokens.EnsureValidStatementEnding()";
    }
}
