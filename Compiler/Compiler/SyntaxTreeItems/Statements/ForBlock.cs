using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class ForBlock : Statement
    {
        public readonly ForKeywordToken ForKeyword;
        public readonly OpenPerenToken OpenPeren;
        public readonly Statement StartStatement;
        public readonly Expression ContinueExpr;
        public readonly SemicolonToken? SecondSemicolon;
        public readonly Statement RepeatStatement;
        public readonly ClosePerenToken ClosePeren;
        public readonly Statement Body;

        public ForBlock(TokenCollection tokens)
        {
            ForKeyword = tokens.PopToken<ForKeywordToken>();
            OpenPeren = tokens.PopToken<OpenPerenToken>();
            StartStatement = Statement.ReadStatement(tokens);
            ContinueExpr = Expression.ReadExpression(tokens);
            SecondSemicolon = tokens.EnsureValidStatementEnding();
            RepeatStatement = Statement.ReadStatement(tokens);
            ClosePeren = tokens.PopToken<ClosePerenToken>();
            Body = Statement.ReadStatement(tokens);
        }
}
}
