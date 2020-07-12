using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ModifierList
    {
        public readonly Token[] Modifiers;

        public ModifierList(TokenCollection tokens)
        {
            LinkedList<Token> modifiers = new LinkedList<Token>();
            while (tokens.PopIfMatches(out Token token, TokenType.Modifier))
            {
                modifiers.AddLast(token);
            }
            Modifiers = modifiers.ToArray();
        }
    }
}
