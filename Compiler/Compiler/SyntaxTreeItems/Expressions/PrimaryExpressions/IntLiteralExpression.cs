using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class IntLiteralExpression : PrimaryExpression
    {
        public readonly IntLiteralToken IntLiteral;

        public override IToken LeftToken => IntLiteral;
        public override IToken RightToken => IntLiteral;

        public IntLiteralExpression(TokenCollection tokens, IntLiteralToken? intLiteral = null)
        {
            IntLiteral = intLiteral == null ? tokens.PopToken<IntLiteralToken>() : (IntLiteralToken)intLiteral;
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
