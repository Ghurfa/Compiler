using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class FalseLiteral
    {
        public readonly Token FalseKeyword;
        public FalseLiteral(TokenCollection tokens, Token falseKeyword)
        {
            FalseKeyword = falseKeyword;
        }
    }
}
