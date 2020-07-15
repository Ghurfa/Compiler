using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Modifiers
{
    class IfNotTypeModifier : CheckModifier
    {
        public string Type;
        public string Identifier;
        public IfNotTypeModifier(string[] arguments)
        {
            if (arguments.Length != 2) throw new NotImplementedException();
            Identifier = arguments[0];
            Type = arguments[1];
        }

        public override string GetCondition()
        {
            return $"!({Identifier} is {Type})";
        }
    }
}
