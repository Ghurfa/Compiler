using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Modifiers
{
    class UntilPopModifier : WhileModifier
    {
        public string TokenToPop;
        public UntilPopModifier(string[] arguments)
        {
            if (arguments.Length != 1) throw new NotImplementedException();
            TokenToPop = arguments[0];
        }

        public override string GetCondition()
        {
            return $"!tokens.PopIfMatches(out {TokenToPop})";
        }
    }
}
