using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class TrueLiteralExpression : PrimaryExpression
    {
        public TrueKeywordToken TrueKeyword { get; private set; }

        public TrueLiteralExpression(TokenCollection tokens, TrueKeywordToken trueKeyword)
        {
            TrueKeyword = trueKeyword;
        }

        public override string ToString()
        {
            string ret = "";
            ret += TrueKeyword.ToString();
            return ret;
        }
    }
}
