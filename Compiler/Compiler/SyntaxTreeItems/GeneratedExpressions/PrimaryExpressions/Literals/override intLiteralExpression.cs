using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class override intLiteralExpression : PrimaryExpression
    {
        public override intLiteral override intLiteral { get; private set; }

        public override intLiteralExpression(TokenCollection tokens, override intLiteral override intLiteral = null)
        {
            override intLiteral = override intLiteral == null ? new override intLiteral(tokens) : override intLiteral;
        }

        public override string ToString()
        {
            string ret = "";
            ret += override intLiteral.ToString();
            return ret;
        }
    }
}
