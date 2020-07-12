using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems.Types
{
    public class TupleType : Type
    {
        public readonly Token OpenPeren;
        public readonly TupleTypeItem[] Items;
        public readonly Token ClosePeren;
        public TupleType(TokenCollection tokens)
        {
            OpenPeren = tokens.PopToken(TokenType.OpenPeren);

            var items = new LinkedList<TupleTypeItem>();
            bool lastMissingComma = false;
            while (tokens.PeekToken().Type != TokenType.ClosePeren)
            {
                if (lastMissingComma) throw new UnexpectedToken(tokens.PeekToken());
                var newItem = new TupleTypeItem(tokens);
                items.AddLast(newItem);
                lastMissingComma = newItem.Comma == null;
            }
            Items = items.ToArray();

            ClosePeren = tokens.PopToken(TokenType.ClosePeren);
        }
    }
    public class TupleTypeItem
    {
        public readonly Token? Name;
        public readonly Token? Colon;
        public readonly Type Type;
        public readonly Token? Comma;

        public TupleTypeItem(TokenCollection tokens)
        {
            if (tokens.PopIfMatches(out Token identifier, TokenType.Identifier))
            {
                if (tokens.PopIfMatches(out Token colon, TokenType.Colon))
                {
                    Name = identifier;
                    Colon = colon;
                    Type = Type.ReadType(tokens);
                }
                else
                {
                    Type = new QualifiedIdentifier(tokens, identifier);
                }
            }
            else
            {
                Type = Type.ReadType(tokens);
            }
            if (tokens.PopIfMatches(out Token comma, TokenType.Comma))
            {
                Comma = comma;
            }
        }
    }
}
