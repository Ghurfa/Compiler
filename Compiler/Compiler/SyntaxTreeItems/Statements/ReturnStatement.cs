using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class ReturnStatement : Statement
    {
        public readonly ReturnKeywordToken ReturnKeyword;
        public readonly OpenPerenToken OpenPeren;
        public readonly Expression Expression;
        public readonly ClosePerenToken ClosePeren;

        public ReturnStatement(TokenCollection tokens)
        {
            ReturnKeyword = tokens.PopToken<ReturnKeywordToken>();
            OpenPeren = tokens.PopToken<OpenPerenToken>();
            Expression = Expression.ReadExpression(tokens);
            ClosePeren = tokens.PopToken<ClosePerenToken>();
        }
    }
}
