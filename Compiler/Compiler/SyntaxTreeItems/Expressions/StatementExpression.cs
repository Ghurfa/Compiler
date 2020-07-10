using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class StatementExpression : Expression
    {
        public static StatementExpression ReadStatementExpression(LinkedList<Token> tokens)
        {
            PrimaryExpression startExpr = PrimaryExpression.ReadPrimaryExpression(tokens);
            if(tokens.PopIfMatches(out Token identifierToken, TokenType.Identifier))
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
