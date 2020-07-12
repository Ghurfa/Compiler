using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public abstract class ClassItemDeclaration
    {
        public static ClassItemDeclaration ReadClassItem(TokenCollection tokens)
        {
            if (tokens.PopIfMatches(out Token constructorToken, TokenType.ConstructorKeyword))
            {
                return new ConstructorDeclaration(tokens, constructorToken);
            }
            else if (tokens.PopIfMatches(out Token identifierToken, TokenType.Identifier))
            {
                if (tokens.PopIfMatches(out Token syntaxChar, TokenType.Colon) ||
                    tokens.PopIfMatches(out syntaxChar, TokenType.Assign) ||
                    tokens.PopIfMatches(out syntaxChar, TokenType.DeclAssign))
                {
                    return new FieldDeclaration(tokens, identifierToken, syntaxChar);
                }
                else
                {
                    return new MethodDeclaration(tokens, identifierToken);
                }
            }
            else throw new UnexpectedToken(tokens.PeekToken());
        }
    }
}
