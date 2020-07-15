using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ModifierList
    {
        public readonly ModifierToken[] Modifiers;

        public  IToken LeftToken => Modifiers.First();
        public  IToken RightToken => Modifiers.Last();

        public ModifierList(TokenCollection tokens, ModifierToken[] modifiers = null)
        {
            var modifiersList = new LinkedList<ModifierToken>();
            if (modifiers != null)
            {
                foreach (var item in modifiers)
                {
                    modifiersList.AddLast(item);
                }
            }
            while(tokens.PeekToken() is ModifierToken)
            {
                var newItem = tokens.PopToken<ModifierToken>();
                modifiersList.AddLast(newItem);
            }
            Modifiers = modifiersList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            return ret;
        }
    }
}
