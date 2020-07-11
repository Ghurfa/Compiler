using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class ReturnStatement : Statement
    {
        public readonly Token ReturnKeyword;
        public readonly Token OpenPeren;
        public readonly Expression Expression;
        public readonly Token ClosePeren;

        public ReturnStatement(TokenCollection tokens)
        {
            ReturnKeyword = tokens.PopToken(TokenType.ReturnKeyword);
            OpenPeren = tokens.PopToken(TokenType.OpenPeren);
            Expression = Expression.ReadExpression(tokens);
            ClosePeren = tokens.PopToken(TokenType.ClosePeren);
        }
    }
}
