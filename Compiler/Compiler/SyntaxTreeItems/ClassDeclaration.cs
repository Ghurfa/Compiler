using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class ClassDeclaration
    {
        public readonly Token Identifier;
        public readonly Token[] AccessModifiers;
        public readonly Token ClassKeyword;
        public readonly Token OpenBrace;
        public readonly ClassItemDeclaration[] ClassItemDeclarations;
        public readonly Token CloseBrace;
        public ClassDeclaration(LinkedList<Token> tokens)
        {
            Identifier = tokens.GetToken(TokenType.Identifier);
            AccessModifiers = tokens.GetModifiers();
            ClassKeyword = tokens.GetToken(TokenType.Keyword, "class");
            OpenBrace = tokens.GetToken(TokenType.SyntaxChar, "{");

            LinkedList<ClassItemDeclaration> items = new LinkedList<ClassItemDeclaration>();
            while (!tokens.PopIfMatches(out CloseBrace, TokenType.SyntaxChar, "}"))
            {
                items.AddLast(createClassItemDeclaration(tokens));
            }
            ClassItemDeclarations = items.ToArray();
        }
        private ClassItemDeclaration createClassItemDeclaration(LinkedList<Token> tokens)
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
