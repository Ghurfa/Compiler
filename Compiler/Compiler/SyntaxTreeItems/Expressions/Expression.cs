using Compiler.SyntaxTreeItems.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public abstract class Expression
    {
        public static Expression ReadExpression(LinkedList<Token> tokens)
        {
            if (tokens.PopIfMatches(out Token intToken, TokenType.IntLiteral))
            {
                return new IntLiteral(tokens, intToken);
            }
            else if (tokens.PopIfMatches(out Token strOpenToken, TokenType.SyntaxChar, "\""))
            {
                return new StringLiteral(tokens, strOpenToken);
            }
            else if (tokens.PopIfMatches(out Token charOpenToken, TokenType.SyntaxChar, "'"))
            {
                return new CharLiteral(tokens, charOpenToken);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
