using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class ReturnStatement : Statement
    {
        public readonly IToken ReturnKeyword;
        public readonly IToken OpenPeren;
        public readonly Expression Expression;
        public readonly IToken ClosePeren;

        public ReturnStatement(TokenCollection tokens)
        {
            ReturnKeyword = tokens.PopToken(TokenType.ReturnKeyword);
            OpenPeren = tokens.PopToken(TokenType.OpenPeren);
            Expression = Expression.ReadExpression(tokens);
            ClosePeren = tokens.PopToken(TokenType.ClosePeren);
        }
    }
}
