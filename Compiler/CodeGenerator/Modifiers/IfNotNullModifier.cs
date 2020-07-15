using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Modifiers
{
    class IfNotNullModifier : CheckModifier
    {
        public string Expression;
        public IfNotNullModifier(string[] arguments)
        {
            if (arguments.Length != 1) throw new NotImplementedException();
            Expression = arguments[0];
        }

        public override string GetCondition()
        {
            return $"{Expression} != null";
        }
    }
}
