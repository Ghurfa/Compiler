using CodeGenerator.Modifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.SyntaxTreeItemsFieldInfos
{
    class TokenFieldInfo : FieldInfo
    {
        public SpecialValueModifier SpecialVal;
        public TokenFieldInfo(string type, string name, InitialModifier modifier = null) : base(type, name)
        {
            if(modifier != null)
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
            if(SpecialVal == null)
            {
                return new string[] { $"{Name} = {LowerCaseName} == null ? tokens.PopToken<{Type}>() : ({Type}){LowerCaseName};" };
            }
            else
            {
                return new string[] { $"{Name} = {LowerCaseName} == null ? {SpecialVal.GetSpecialValueExpression()} : ({Type}){LowerCaseName};" };
            }
        }
    }
}
