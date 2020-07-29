using System;
using System.Collections.Generic;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems.Statements
{
    public class IfBlock : Statement
    {
        public readonly IfKeywordToken IfKeyword;
        public readonly Expression Condition;
        public readonly Statement IfTrue;
        public readonly ElseKeywordToken? ElseToken;
        public readonly Statement IfFalse;

        public IfBlock(TokenCollection tokens)
        {
            IfKeyword = tokens.PopToken<IfKeywordToken>();
            Condition = Expression.ReadExpression(tokens);
            if (!(Condition is PerenthesizedExpression))
            {
                tokens.EnsureWhitespaceAfter(IfKeyword);
            }

            IToken tokenAfterCondition = tokens.PeekToken();
            IfTrue = Statement.ReadStatement(tokens);
            if (!(Condition is PerenthesizedExpression) && !(IfTrue is CodeBlock) &&
                !(tokenAfterCondition is WhitespaceWithLineBreakToken))
            {
                tokens.EnsureLineBreakAfter(tokenAfterCondition);
            }

            if(tokens.PopIfMatches(out ElseKeywordToken elseToken))
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
