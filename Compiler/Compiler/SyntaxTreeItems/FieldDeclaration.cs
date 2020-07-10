using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class FieldDeclaration : ClassItemDeclaration
    {
        public readonly Token Identifier;
        public readonly Token? AssignmentToken;
        public readonly Expression DefaultValue;
        public readonly Token? ColonToken;
        public readonly Token[] AccessModifiers;
        public readonly TypeToken Type;
        public FieldDeclaration(LinkedList<Token> tokens, Token identifierToken, Token syntaxCharToken)
        {
            Identifier = identifierToken;
            
            if(syntaxCharToken.Text == ":=")
            {
                AssignmentToken = syntaxCharToken;
                DefaultValue = Expression.ReadExpression(tokens);
                if(tokens.PopIfMatches(out Token colon, TokenType.SyntaxChar, ":"))
                {
                    ColonToken = colon;
                    AccessModifiers = tokens.GetModifiers();
                }
            }
            else
            {
                if (syntaxCharToken.Text == "=")
                {
                    AssignmentToken = syntaxCharToken;
                    DefaultValue = Expression.ReadExpression(tokens);
                    ColonToken = tokens.GetToken(TokenType.SyntaxChar, ":");
                }
                else
                {
                    AssignmentToken = null;
                    DefaultValue = null;
                    ColonToken = syntaxCharToken;
                }
                AccessModifiers = tokens.GetModifiers();
                Type = new TypeToken(tokens);
            }
            
        }
    }
}
