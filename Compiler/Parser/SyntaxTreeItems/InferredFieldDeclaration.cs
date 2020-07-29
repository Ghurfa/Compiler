using System;
using System.Collections.Generic;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems.ClassItemDeclarations
{
    public class InferredFieldDeclaration : FieldDeclaration
    {
        public DeclAssignToken DeclAssign { get; protected set; }
        public Expression DefaultValue { get; protected set; }
        public ColonToken? Colon { get; protected set; }

        public InferredFieldDeclaration(TokenCollection tokens, IdentifierToken identifier, DeclAssignToken declAssign) : base(tokens, identifier)
        {
            DeclAssign = declAssign;
            DefaultValue = Expression.ReadExpression(tokens);
            if (tokens.PopIfMatches(out ColonToken colon))
            {
                Colon = colon;
                Modifiers = new ModifierList(tokens);
            }
        }
    }
}
