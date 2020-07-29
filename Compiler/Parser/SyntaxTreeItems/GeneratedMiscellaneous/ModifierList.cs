using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class ModifierList
    {
        public ModifierToken[] Modifiers { get; private set; }

        public ModifierList(TokenCollection tokens)
        {
            LinkedList<ModifierToken> modifiersList = new LinkedList<ModifierToken>();
            while (tokens.PeekToken() is ModifierToken)
            {
                var add = tokens.PopToken<ModifierToken>();
                modifiersList.AddLast(add);
            }
            Modifiers = modifiersList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            foreach (var item in Modifiers)
            {
                ret += item.ToString();
            }
            return ret;
        }
    }
}
