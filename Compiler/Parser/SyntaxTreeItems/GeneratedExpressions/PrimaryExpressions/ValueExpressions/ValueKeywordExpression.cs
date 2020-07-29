using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class ValueKeywordExpression : PrimaryExpression
    {
        public ValueKeywordToken ValueKeyword { get; private set; }

        public ValueKeywordExpression(TokenCollection tokens, ValueKeywordToken valueKeyword)
        {
            ValueKeyword = valueKeyword;
        }

        public override string ToString()
        {
            string ret = "";
            ret += ValueKeyword.ToString();
            return ret;
        }
    }
}
