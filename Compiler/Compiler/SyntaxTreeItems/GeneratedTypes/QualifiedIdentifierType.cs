using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;

namespace Compiler.SyntaxTreeItems
{
    public class QualifiedIdentifierType : Type
    {
        public QualifiedIdentifier Identifier { get; private set; }

        public QualifiedIdentifierType(TokenCollection tokens, QualifiedIdentifier identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Identifier.ToString();
            return ret;
        }
    }
}
