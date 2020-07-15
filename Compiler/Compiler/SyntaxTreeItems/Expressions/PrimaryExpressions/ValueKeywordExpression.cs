using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ValueKeywordExpression : PrimaryExpression
    {
        public readonly ValueKeywordToken ValueKeyword;

        public override IToken LeftToken => ValueKeyword;
        public override IToken RightToken => ValueKeyword;

        public ValueKeywordExpression(TokenCollection tokens, ValueKeywordToken? valueKeyword = null)
        {
            ValueKeyword = valueKeyword == null ? tokens.PopToken<ValueKeywordToken>() : (ValueKeywordToken)valueKeyword;
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
