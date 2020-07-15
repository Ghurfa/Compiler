using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.ClassItemDeclarations
{
    public class SimpleFieldDeclaration : FieldDeclaration
    {
        public ColonToken Colon { get; protected set; }
        public Type Type { get; protected set; }
        public AssignToken? EqualsSign { get; protected set; }
        public Expression DefaultValue { get; protected set; }
        public SimpleFieldDeclaration(TokenCollection tokens, IdentifierToken identifier, ColonToken colon) : base(tokens, identifier)
        {
            Colon = colon;
            Modifiers = new ModifierList(tokens);
            Type = Type.ReadType(tokens);

            if(tokens.PopIfMatches(out AssignToken assign))
            {
                EqualsSign = assign;
                DefaultValue = Expression.ReadExpression(tokens);
            }

            Semicolon = tokens.EnsureValidStatementEnding();
        }
    }
}
