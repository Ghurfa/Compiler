using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ReturnStatement : Statement
    {
        public readonly ReturnKeywordToken ReturnKeyword;
        public readonly Expression Expression;

        public override IToken LeftToken => ReturnKeyword;
        public override IToken RightToken => Expression.RightToken;

        public ReturnStatement(TokenCollection tokens, ReturnKeywordToken? returnKeyword = null, Expression expression = null)
        {
            ReturnKeyword = returnKeyword == null ? tokens.PopToken<ReturnKeywordToken>() : (ReturnKeywordToken)returnKeyword;
            Expression = expression == null ? Expression.ReadExpression(tokens) : expression;
            tokens.EnsureWhitespaceAfter(ReturnKeyword); 
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
