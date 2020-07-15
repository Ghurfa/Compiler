using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Modifiers
{
    abstract class CheckModifier : Modifier
    {
        public abstract string GetCondition();
    }
}
