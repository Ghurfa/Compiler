using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Modifiers
{
    class WhilePopTypeModifier : WhileModifier
    {
        public string TokenType;
        public WhilePopTypeModifier(string[] arguments)
        {
            if (arguments.Length != 1) throw new NotImplementedException();
            TokenType = arguments[0];
        }

        public override string GetCondition()
        {
            return $"tokens.PeekToken() is {TokenType}";
        }
    }
}
