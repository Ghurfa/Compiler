using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ModifierList
    {
        public readonly ModifierToken[] Modifiers;

        public ModifierList(TokenCollection tokens)
        {
            LinkedList<ModifierToken> modifiers = new LinkedList<ModifierToken>();
            while (tokens.PopIfMatches(out ModifierToken token))
            {
                modifiers.AddLast(token);
            }
            Modifiers = modifiers.ToArray();
        }
    }
}
