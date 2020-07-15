using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class QualifiedIdentifierType : Type
    {
        public readonly QualifiedIdentifier Identifier;

        public override IToken LeftToken => Identifier.LeftToken;
        public override IToken RightToken => Identifier.RightToken;

        public QualifiedIdentifierType(TokenCollection tokens, QualifiedIdentifier identifier = null)
        {
            Identifier = identifier == null ? new QualifiedIdentifier(tokens) : identifier;
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
