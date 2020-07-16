using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ValueKeywordExpression : PrimaryExpression
    {
        public ValueKeywordToken ValueKeyword { get; private set; }

        public ValueKeywordExpression(TokenCollection tokens, ValueKeywordToken? valueKeyword = null)
        {
            ValueKeyword = valueKeyword == null ? tokens.PopToken<ValueKeywordToken>() : (ValueKeywordToken)valueKeyword;
        }

        public override string ToString()
        {
            string ret = "";
            ret += ValueKeyword.ToString();
            return ret;
        }
    }
}
