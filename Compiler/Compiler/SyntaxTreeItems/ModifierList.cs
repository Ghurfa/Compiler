using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ModifierList
    {
        public readonly IToken[] Modifiers;

        public ModifierList(TokenCollection tokens)
        {
            LinkedList<IToken> modifiers = new LinkedList<IToken>();
            while (tokens.PopIfMatches(out IToken token, TokenType.Modifier))
            {
                modifiers.AddLast(token);
            }
            Modifiers = modifiers.ToArray();
        }
    }
}
