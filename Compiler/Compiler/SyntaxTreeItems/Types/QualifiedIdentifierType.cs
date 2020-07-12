using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems.Types
{
    public class QualifiedIdentifierType : Type
    {
        public readonly QualifiedIdentifier Identifier;
        public QualifiedIdentifierType(TokenCollection tokens)
        {
            Identifier = new QualifiedIdentifier(tokens);
        }
        public QualifiedIdentifierType(TokenCollection tokens, Token firstIdentifier)
        {
            Identifier = new QualifiedIdentifier(tokens, firstIdentifier);
        }
        public override string ToString()
        {
            return Identifier.ToString();
        }
    }
}
