using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Modifiers
{
    abstract class WhileModifier : InitialModifier
    {
        public abstract string GetCondition();
    }
}
