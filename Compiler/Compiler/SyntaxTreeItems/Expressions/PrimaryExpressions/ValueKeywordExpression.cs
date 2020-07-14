using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class ValueKeywordExpression : PrimaryExpression
    {
        public readonly IToken ValueKeyword;
        public ValueKeywordExpression(TokenCollection tokens, IToken keyword)
        {
            ValueKeyword = keyword;
        }
        public override string ToString()
        {
            return ValueKeyword.ToString();
        }
    }
}
