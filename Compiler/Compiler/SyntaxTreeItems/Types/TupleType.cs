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
                if (lastMissingComma) throw new InvalidTokenException(tokens.PeekToken());
                var newItem = new TupleTypeItem(tokens);
                items.AddLast(newItem);
                lastMissingComma = newItem.Comma == null;
            }
            Items = items.ToArray();

            ClosePeren = tokens.PopToken(TokenType.ClosePeren);
        }
        public override string ToString()
        {
            string ret = OpenPeren.ToString();
            for(int i = 0; i < Items.Length; i++)
            {
                ret += Items[i].ToString();
                if (i < Items.Length - 1) ret += " ";
            }
            return ret + ClosePeren.ToString();
        }
    }
}
