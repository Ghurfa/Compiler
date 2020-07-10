using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public abstract class ClassItemDeclaration
    {
        public static ClassItemDeclaration ReadClassItem(LinkedList<Token> tokens)
        {
            Token firstToken = tokens.GetToken();
            if (firstToken.Type == TokenType.BlockMarker)
            {
                if (firstToken.Text != "ctor") throw new SyntaxTreeBuildingException(firstToken);
                return new ConstructorDeclaration(tokens, firstToken);
            }
            else if (firstToken.Type == TokenType.Identifier)
            {
                Token identifier = firstToken;
                if (tokens.PopIfMatches(out Token syntaxChar, TokenType.SyntaxChar, ":") ||
                    tokens.PopIfMatches(out syntaxChar, TokenType.Operator, "=") ||
                    tokens.PopIfMatches(out syntaxChar, TokenType.Operator, ":="))
                {
                    return new FieldDeclaration(tokens, identifier, syntaxChar);
                }
                else
                {
                    return new MethodDeclaration(tokens, identifier);
                }
            }
            else throw new SyntaxTreeBuildingException(firstToken);
        }
    }
}
