using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class WhileBlock : Statement
    {
        public readonly Token WhileKeyword;
        public readonly Expression Condition;
        public readonly Statement Statement;

        public WhileBlock(TokenCollection tokens)
        {
            WhileKeyword = tokens.PopToken(TokenType.WhileKeyword);
            Condition = Expression.ReadExpression(tokens);
            Statement = Statement.ReadStatement(tokens);
        }
    }
}
