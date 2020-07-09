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
            if (firstToken.Type == TokenType.Keyword)
            {
                if (firstToken.Text != "ctor") throw new SyntaxTreeBuildingException(firstToken);
                return new ConstructorDeclaration(tokens, firstToken);
            }
            else if (firstToken.Type == TokenType.Identifier)
            {
                Token identifier = firstToken;
                Token[] modifiers = tokens.GetModifiers();
                TypeToken type = new TypeToken(tokens);
                Token syntaxChar = tokens.GetToken(TokenType.SyntaxChar);
                if (syntaxChar.Text == "(")
                {
                    return new MethodDeclaration(tokens, identifier, modifiers, type, syntaxChar);
                }
                else if (syntaxChar.Text == "{")
                {
                    throw new NotImplementedException();
                }
                else
                {
                    return new FieldDeclaration(tokens, identifier, modifiers, type, syntaxChar);
                }
            }
            else throw new SyntaxTreeBuildingException(firstToken);
        }
    }
}
