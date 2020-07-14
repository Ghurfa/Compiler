using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions
{
    public class FalseLiteral : PrimaryExpression
    {
        public readonly IToken FalseKeyword;
        public FalseLiteral(TokenCollection tokens, IToken falseKeyword)
        {
            FalseKeyword = falseKeyword;
        }
        public override string ToString()
        {
            return FalseKeyword.ToString();
        }
    }
}
