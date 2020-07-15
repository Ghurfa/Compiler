using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class TupleExpression : PrimaryExpression
    {
        public readonly OpenPerenToken OpenPeren;
        public readonly TupleItemList Values;
        public readonly ClosePerenToken ClosePeren;

        public override IToken LeftToken => OpenPeren;
        public override IToken RightToken => ClosePeren;

        public TupleExpression(TokenCollection tokens, OpenPerenToken? openPeren = null, TupleItemList values = null, ClosePerenToken? closePeren = null)
        {
            OpenPeren = openPeren == null ? tokens.PopToken<OpenPerenToken>() : (OpenPerenToken)openPeren;
            Values = values == null ? new TupleItemList(tokens) : values;
            ClosePeren = closePeren == null ? tokens.PopToken<ClosePerenToken>() : (ClosePerenToken)closePeren;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
