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
        public readonly SyntaxTreeItems.Type Type;
        public FieldDeclaration(TokenCollection tokens, Token identifierToken, Token syntaxCharToken)
        {
            Identifier = identifierToken;
            
            if(syntaxCharToken.Text == ":=")
            {
                AssignmentToken = syntaxCharToken;
                DefaultValue = Expression.ReadExpression(tokens);
                if(tokens.PopIfMatches(out Token colon, TokenType.Colon))
                {
                    ColonToken = colon;
                    AccessModifiers = tokens.ReadModifiers();
                }
            }
            else
            {
                if (syntaxCharToken.Text == "=")
                {
                    AssignmentToken = syntaxCharToken;
                    DefaultValue = Expression.ReadExpression(tokens);
                    ColonToken = tokens.PopToken(TokenType.Colon);
                }
                else
                {
                    AssignmentToken = null;
                    DefaultValue = null;
                    ColonToken = syntaxCharToken;
                }
                AccessModifiers = tokens.ReadModifiers();
                Type = SyntaxTreeItems.Type.ReadType(tokens);
            }
            
        }
    }
}
