using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ThrowStatement : Statement
    {
        public readonly ThrowKeywordToken ThrowKeyword;
        public readonly Expression Exception;
        public readonly SemicolonToken? Semicolon;

        public override IToken LeftToken => ThrowKeyword;
        public override IToken RightToken => Semicolon.RightToken;

        public ThrowStatement(TokenCollection tokens, ThrowKeywordToken? throwKeyword = null, Expression exception = null, SemicolonToken? semicolon = null)
        {
            ThrowKeyword = throwKeyword == null ? tokens.PopToken<ThrowKeywordToken>() : (ThrowKeywordToken)throwKeyword;
            Exception = exception == null ? Expression.ReadExpression(tokens) : exception;
            Semicolon = semicolon == null ? tokens.EnsureValidStatementEnding() : semicolon;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
