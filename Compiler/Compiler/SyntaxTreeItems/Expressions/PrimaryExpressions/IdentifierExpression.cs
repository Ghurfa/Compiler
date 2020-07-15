using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class IdentifierExpression : PrimaryExpression
    {
        public readonly IdentifierToken Identifier;

        public override IToken LeftToken => Identifier;
        public override IToken RightToken => Identifier;

        public IdentifierExpression(TokenCollection tokens, IdentifierToken? identifier = null)
        {
            Identifier = identifier == null ? tokens.PopToken<IdentifierToken>() : (IdentifierToken)identifier;
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
