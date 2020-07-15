using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class TupleType : Type
    {
        public readonly OpenPerenToken OpenPeren;
        public readonly TupleTypeItemList Items;
        public readonly ClosePerenToken ClosePeren;

        public override IToken LeftToken => OpenPeren;
        public override IToken RightToken => ClosePeren;

        public TupleType(TokenCollection tokens, OpenPerenToken? openPeren = null, TupleTypeItemList items = null, ClosePerenToken? closePeren = null)
        {
            OpenPeren = openPeren == null ? tokens.PopToken<OpenPerenToken>() : (OpenPerenToken)openPeren;
            Items = items == null ? new TupleTypeItemList(tokens) : items;
            ClosePeren = closePeren == null ? tokens.PopToken<ClosePerenToken>() : (ClosePerenToken)closePeren;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
