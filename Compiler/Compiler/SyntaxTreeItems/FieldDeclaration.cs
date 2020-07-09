using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class FieldDeclaration : ClassItemDeclaration
    {
        public readonly Token Identifier;
        public readonly Token[] AccessModifiers;
        public readonly TypeToken Type;
        public readonly Token SemicolonToken;
        public FieldDeclaration(LinkedList<Token> token, Token identifierToken, Token[] modifierTokens, TypeToken typeToken, Token syntaxCharToken)
        {
            Identifier = identifierToken;
            AccessModifiers = modifierTokens;
            Type = typeToken;
            if(syntaxCharToken.Text == ";")
            {
                SemicolonToken = syntaxCharToken;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
