using System;
using System.Collections.Generic;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class WhileBlock : Statement
    {
        public readonly WhileKeywordToken WhileKeyword;
        public readonly Expression Condition;
        public readonly Statement Body;

        public WhileBlock(TokenCollection tokens)
        {
            WhileKeyword = tokens.PopToken<WhileKeywordToken>();
            Condition = Expression.ReadExpression(tokens);
            if (!(Condition is PerenthesizedExpression))
            {
                tokens.EnsureWhitespaceAfter(WhileKeyword);
            }
            IToken tokenAfterCondition = tokens.PeekToken();
            Body = Statement.ReadStatement(tokens);
            if (!(Condition is PerenthesizedExpression) && !(Body is CodeBlock) &&
                !(tokenAfterCondition is WhitespaceWithLineBreakToken))
            {
                tokens.EnsureLineBreakAfter(tokenAfterCondition);
            }
        }
    }
}
