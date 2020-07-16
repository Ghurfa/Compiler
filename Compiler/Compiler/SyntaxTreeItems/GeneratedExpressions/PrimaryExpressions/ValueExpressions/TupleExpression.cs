using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class TupleExpression : PrimaryExpression
    {
        public OpenPerenToken OpenPeren { get; private set; }
        public TupleItemList Values { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }

        public TupleExpression(TokenCollection tokens, OpenPerenToken? openPeren = null, TupleItemList values = null, ClosePerenToken? closePeren = null)
        {
            OpenPeren = openPeren == null ? tokens.PopToken<OpenPerenToken>() : (OpenPerenToken)openPeren;
            Values = values == null ? new TupleItemList(tokens) : values;
            ClosePeren = closePeren == null ? tokens.PopToken<ClosePerenToken>() : (ClosePerenToken)closePeren;
        }

        public override string ToString()
        {
            string ret = "";
            ret += OpenPeren.ToString();
            ret += Values.ToString();
            ret += ClosePeren.ToString();
            return ret;
        }
    }
}
