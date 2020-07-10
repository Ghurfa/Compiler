using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public abstract class Statement
    {
        public static Statement ReadStatement(TokenCollection tokens)
        {
            if (tokens.PopIfMatches(out Token controlKeyword, TokenType.ControlKeyword))
            {
                throw new NotImplementedException();
            }
            else if (tokens.PopIfMatches(out Token openCurly, TokenType.SyntaxChar, "{"))
            {
                return new CodeBlock(tokens, openCurly);
            }
            else if (tokens.PopIfMatches(out Token semicolon, TokenType.SyntaxChar, ";"))
            {
                return new EmptyStatement(tokens, semicolon);
            }
            else if(tokens.PopIfMatches(out Token operatorToken, TokenType.Operator))
            {
                throw new NotImplementedException();
            }
            else
            {
                Expression startingExpression = Expression.ReadExpression(tokens);
                throw new NotImplementedException();
            }
        }
    }
}
