using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class TupleItemList
    {
        public readonly TupleItem[] Values;

        public  IToken LeftToken => Values.First().LeftToken;
        public  IToken RightToken => Values.Last().RightToken;

        public TupleItemList(TokenCollection tokens, TupleItem[] values = null)
        {
            var valuesList = new LinkedList<TupleItem>();
            if (values != null)
            {
                foreach (var item in values)
                {
                    valuesList.AddLast(item);
                }
            }
            bool lastMissingComma = false;
            while(!lastMissingComma)
            {
                var newItem = new TupleItem(tokens);
                valuesList.AddLast(newItem);
                lastMissingComma = newItem.Comma == null;
            }
            Values = valuesList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            return ret;
        }
    }
}
