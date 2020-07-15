using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class ValueKeywordExpression : PrimaryExpression
    {
        public readonly ValueKeywordToken ValueKeyword;

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
