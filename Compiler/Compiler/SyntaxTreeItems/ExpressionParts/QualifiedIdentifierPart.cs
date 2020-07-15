using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class QualifiedIdentifierPart
    {
        public readonly IdentifierToken Identifier;
        public readonly DotToken? Dot;

        public  IToken LeftToken => Identifier;
        public  IToken RightToken => Dot.RightToken;

        public QualifiedIdentifierPart(TokenCollection tokens, IdentifierToken? identifier = null, DotToken? dot = null)
        {
            Identifier = identifier == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)identifier;
            if(dot == null) tokens.PopIfMatches(out Dot);
            else Dot = (DotToken?)dot;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
