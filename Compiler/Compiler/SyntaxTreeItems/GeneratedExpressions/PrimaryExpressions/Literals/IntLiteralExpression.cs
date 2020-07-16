using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class IntLiteralExpression : PrimaryExpression
    {
        public IntLiteralToken IntLiteral { get; private set; }

        public IntLiteralExpression(TokenCollection tokens, IntLiteralToken? intLiteral = null)
        {
            IntLiteral = intLiteral == null ? tokens.PopToken<IntLiteralToken>() : (IntLiteralToken)intLiteral;
        }

        public override string ToString()
        {
            string ret = "";
            ret += IntLiteral.ToString();
            return ret;
        }
    }
}
