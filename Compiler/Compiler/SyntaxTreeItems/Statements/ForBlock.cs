using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class ForBlock : Statement
    {
        public readonly IToken ForKeyword;
        public readonly IToken OpenPeren;
        public readonly Statement StartStatement;
        public readonly Expression ContinueExpr;
        public readonly IToken? SecondSemicolon;
        public readonly Statement RepeatStatement;
        public readonly IToken ClosePeren;
        public readonly Statement Body;

        public ForBlock(TokenCollection tokens)
        {
            ForKeyword = tokens.PopToken(TokenType.ForKeyword);
            OpenPeren = tokens.PopToken(TokenType.OpenPeren);
            StartStatement = Statement.ReadStatement(tokens);
            ContinueExpr = Expression.ReadExpression(tokens);
            SecondSemicolon = tokens.EnsureValidStatementEnding();
            RepeatStatement = Statement.ReadStatement(tokens);
            ClosePeren = tokens.PopToken(TokenType.ClosePeren);
            Body = Statement.ReadStatement(tokens);
        }
}
}
