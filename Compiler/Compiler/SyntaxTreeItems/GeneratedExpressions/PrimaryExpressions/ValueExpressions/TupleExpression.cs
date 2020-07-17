using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class TupleExpression : PrimaryExpression
    {
        public OpenPerenToken OpenPeren { get; private set; }
        public TupleItemList Values { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }

        public TupleExpression(TokenCollection tokens, OpenPerenToken openPeren, TupleItemList values)
        {
            OpenPeren = openPeren;
            Values = values;
            ClosePeren = tokens.PopToken<ClosePerenToken>();;
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
