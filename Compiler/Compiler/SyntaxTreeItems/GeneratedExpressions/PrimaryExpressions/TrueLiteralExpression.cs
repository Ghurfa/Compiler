using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class TrueLiteralExpression : PrimaryExpression
    {
        public readonly TrueKeywordToken TrueKeyword;

        public TrueLiteralExpression(TokenCollection tokens, TrueKeywordToken? trueKeyword = null)
        {
            TrueKeyword = trueKeyword == null ? tokens.PopToken<TrueKeywordToken>() : (TrueKeywordToken)trueKeyword;
        }

        public override string ToString()
        {
            string ret = "";
            ret += TrueKeyword.ToString();
            return ret;
        }
    }
}
