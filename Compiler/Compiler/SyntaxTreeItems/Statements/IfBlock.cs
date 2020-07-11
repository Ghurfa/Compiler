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
            IfTrue = Statement.ReadStatement(tokens);
            if(tokens.PopIfMatches(out Token elseToken, TokenType.ElseKeyword))
            {
                ElseToken = elseToken;
                IfFalse = Statement.ReadStatement(tokens);
            }
        }
    }
}
