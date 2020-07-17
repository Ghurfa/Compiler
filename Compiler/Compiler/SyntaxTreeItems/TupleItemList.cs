using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class TupleItemList
    {
        public readonly TupleItem[] Items;

        public TupleItemList(TokenCollection tokens, TupleItem initialItem = null)
        {
            var items = new LinkedList<TupleItem>();
            bool lastMissingComma = tokens.PeekToken() is ClosePerenToken;

            if (initialItem != null)
            {
                lastMissingComma = initialItem.Comma == null;
                items.AddLast(initialItem);
            }

            while (!lastMissingComma)
            {
                var item = new TupleItem(tokens);
                items.AddLast(item);
                lastMissingComma = item.Comma == null;
            }
            Items = items.ToArray();
        }
        public override string ToString()
        {
            string ret = "";
            for (int i = 0; i < Items.Length; i++)
            {
                ret += Items[i].ToString();
                if (i < Items.Length - 1) ret += " ";
            }
            return ret;
        }
    }
}
