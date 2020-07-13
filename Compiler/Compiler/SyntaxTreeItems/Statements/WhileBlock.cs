using Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class WhileBlock : Statement
    {
        public readonly Token WhileKeyword;
        public readonly Expression Condition;
        public readonly Statement Body;

        public WhileBlock(TokenCollection tokens)
        {
            WhileKeyword = tokens.PopToken(TokenType.WhileKeyword);
            Condition = Expression.ReadExpression(tokens);
            if (!(Condition is PerenthesizedExpression))
            {
                tokens.EnsureWhitespaceAfter(WhileKeyword);
            }
            Token tokenAfterCondition = tokens.PeekToken();
            Body = Statement.ReadStatement(tokens);
            if (!(Condition is PerenthesizedExpression) && !(Body is CodeBlock) &&
                tokenAfterCondition.Type != TokenType.WhitespaceWithLineBreak)
            {
                tokens.EnsureLineBreakAfter(tokenAfterCondition);
            }
        }
    }
}
