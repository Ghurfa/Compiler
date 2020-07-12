using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class ForBlock : Statement
    {
        public readonly Token ForKeyword;
        public readonly Token OpenPeren;
        public readonly Statement StartStatement;
        public readonly Expression ContinueExpr;
        public readonly Token? SecondSemicolon;
        public readonly Statement RepeatStatement;
        public readonly Token ClosePeren;
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
