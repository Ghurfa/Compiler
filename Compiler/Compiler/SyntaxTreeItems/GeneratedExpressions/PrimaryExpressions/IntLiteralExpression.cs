using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class IntLiteralExpression : PrimaryExpression
    {
        public readonly IntLiteralToken IntLiteral;

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
