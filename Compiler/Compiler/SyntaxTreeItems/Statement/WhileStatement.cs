using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class WhileStatement : Statement
    {
        public readonly WhileKeywordToken WhileKeyword;
        public readonly Expression Condition;
        public readonly Statement Body;

        public override IToken LeftToken => WhileKeyword;
        public override IToken RightToken => Body.RightToken;

        public WhileStatement(TokenCollection tokens, WhileKeywordToken? whileKeyword = null, Expression condition = null, Statement body = null)
        {
            WhileKeyword = whileKeyword == null ? tokens.PopToken<WhileKeywordToken>() : (WhileKeywordToken)whileKeyword;
            Condition = condition == null ? Expression.ReadExpression(tokens) : condition;
            Body = body == null ? Statement.ReadStatement(tokens) : body;
            tokens.EnsureWhitespaceAfter(WhileKeyword); 
            if (!(Body is CodeBlock))
            {
                tokens.EnsureLineBreakAfter(Condition);
            }
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            
            
            return ret;
        }
    }
}
