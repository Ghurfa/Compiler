using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Modifiers
{
    class UntilModifier : WhileModifier
    {
        public string ConditionName;
        public UntilModifier(string[] arguments)
        {
            if (arguments.Length != 1) throw new NotImplementedException();
            ConditionName = arguments[0];
        }

        public override string GetCondition()
        {
            return $"!{ConditionName}";
        }
    }
}
