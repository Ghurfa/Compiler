using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class TupleTypeItemList
    {
        public readonly TupleTypeItem[] Items;

        public  IToken LeftToken => Items.First().LeftToken;
        public  IToken RightToken => Items.Last().RightToken;

        public TupleTypeItemList(TokenCollection tokens, TupleTypeItem[] items = null)
        {
            var itemsList = new LinkedList<TupleTypeItem>();
            if (items != null)
            {
                foreach (var item in items)
                {
                    itemsList.AddLast(item);
                }
            }
            bool lastMissingComma = false;
            while(!lastMissingComma)
            {
                var newItem = new TupleTypeItem(tokens);
                itemsList.AddLast(newItem);
                lastMissingComma = newItem.Comma == null;
            }
            Items = itemsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            return ret;
        }
    }
}
