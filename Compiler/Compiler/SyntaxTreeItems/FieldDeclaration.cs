using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class FieldDeclaration : ClassItemDeclaration
    {
        public readonly Token Name;
        
        public readonly Token? ColonToken;
        public readonly ModifierList Modifiers;
        public readonly SyntaxTreeItems.Type Type;

        public readonly Token? AssignmentToken;
        public readonly Expression DefaultValue;
        public FieldDeclaration(TokenCollection tokens, Token identifierToken, Token syntaxCharToken)
        {
            Name = identifierToken;
            
            if(syntaxCharToken.Type == TokenType.DeclAssign)
            {
                AssignmentToken = syntaxCharToken;
                DefaultValue = Expression.ReadExpression(tokens);
                if(tokens.PopIfMatches(out Token colon, TokenType.Colon))
                {
                    ColonToken = colon;
                    Modifiers = new ModifierList(tokens);
                }
            }
            else
            {
                ColonToken = syntaxCharToken;
                Modifiers = new ModifierList(tokens);
                Type = SyntaxTreeItems.Type.ReadType(tokens);

                if(tokens.PopIfMatches(out Token equals, TokenType.Assign))
                {
                    AssignmentToken = equals;
                    DefaultValue = Expression.ReadExpression(tokens);
                }
            }
            
        }
    }
}
