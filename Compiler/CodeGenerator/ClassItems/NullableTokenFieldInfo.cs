using CodeGenerator.Modifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.ClassItems
{
    class NullableTokenFieldInfo : FieldInfo
    {
        public NullableTokenFieldInfo(string type, string name) : base(type, name) { }

        public SpecialValueModifier SpecialVal;
        public NullableTokenFieldInfo(string type, string name, InitialModifier modifier= null) : base(type, name)
        {
            if (modifier != null)
            {
                if (modifier is SpecialValueModifier specialVal)
                {
                    SpecialVal = specialVal;
                }
                else throw new InvalidOperationException();
            }
        }
        public override string[] GetCreationStatements()
        {
            if (SpecialVal == null)
            {
                return new string[] { $"if({LowerCaseName} == null) tokens.PopIfMatches(out {Name});",
                                      $"else {Name} = ({Type}){LowerCaseName};" };
            }
            else
            {
                return new string[] { $"{Name} = {LowerCaseName} == null ? {SpecialVal.GetSpecialValueExpression()} : {LowerCaseName};" };
            }
        }
    }
}
