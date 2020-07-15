using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class FieldDeclaration : ClassItemDeclaration
    {
        public readonly IdentifierToken Name;
        
        public readonly ColonToken? ColonToken;
        public readonly ModifierList Modifiers;
        public readonly Type Type;

        public readonly IToken AssignmentToken;
        public readonly Expression DefaultValue;

        public readonly SemicolonToken? Semicolon;

        public override IToken LeftToken => Name;
        public override IToken RightToken => Semicolon ?? DefaultValue.RightToken;

        public FieldDeclaration(TokenCollection tokens, IdentifierToken identifier, IToken syntaxCharToken)
        {
            Name = identifier;

            if (syntaxCharToken is DeclAssignToken declAssign)
            {
                AssignmentToken = declAssign;
                DefaultValue = Expression.ReadExpression(tokens);
                if (tokens.PopIfMatches(out ColonToken colon))
                {
                    ColonToken = colon;
                    Modifiers = new ModifierList(tokens);
                }
            }
            else if (syntaxCharToken is ColonToken colon)
            {
                ColonToken = colon;
                Modifiers = new ModifierList(tokens);
                Type = Type.ReadType(tokens);

                if (tokens.PopIfMatches(out AssignToken equals))
                {
                    AssignmentToken = equals;
                    DefaultValue = Expression.ReadExpression(tokens);
                }
            }
            else throw new InvalidTokenException(syntaxCharToken);

            Semicolon = tokens.EnsureValidStatementEnding();
        }
    }
}
