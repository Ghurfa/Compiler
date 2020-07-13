using Compiler.SyntaxTreeItems.Expressions.PrimaryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class IfBlock : Statement
    {
        public readonly Token IfKeyword;
        public readonly Expression Condition;
        public readonly Statement IfTrue;
        public readonly Token? ElseToken;
        public readonly Statement IfFalse;

        public IfBlock(TokenCollection tokens)
        {
            IfKeyword = tokens.PopToken(TokenType.IfKeyword);
            Condition = Expression.ReadExpression(tokens);
            if (!(Condition is PerenthesizedExpression))
            {
                tokens.EnsureWhitespaceAfter(IfKeyword);
            }

            Token tokenAfterCondition = tokens.PeekToken();
            IfTrue = Statement.ReadStatement(tokens);
            if (!(Condition is PerenthesizedExpression) && !(IfTrue is CodeBlock) &&
                tokenAfterCondition.Type != TokenType.WhitespaceWithLineBreak)
            {
                tokens.EnsureLineBreakAfter(tokenAfterCondition);
            }

            if(tokens.PopIfMatches(out Token elseToken, TokenType.ElseKeyword))
            {
                ElseToken = elseToken;
                IfFalse = Statement.ReadStatement(tokens);
                if(!(IfFalse is CodeBlock))
                {
                    tokens.EnsureWhitespaceAfter(elseToken);
                }
            }
        }
    }
}
