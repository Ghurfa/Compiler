using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class IntLiteralExpression : PrimaryExpression
    {
        public IntLiteralToken IntLiteral { get; private set; }

        public IntLiteralExpression(TokenCollection tokens, IntLiteralToken intLiteral)
        {
            IntLiteral = intLiteral;
        }

        public override string ToString()
        {
            string ret = "";
            ret += IntLiteral.ToString();
            return ret;
        }
    }
}
