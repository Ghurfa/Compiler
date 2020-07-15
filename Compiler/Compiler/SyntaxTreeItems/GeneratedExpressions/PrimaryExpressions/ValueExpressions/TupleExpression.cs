using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class TupleExpression : PrimaryExpression
    {
        public readonly OpenPerenToken OpenPeren;
        public readonly TupleItemList Values;
        public readonly ClosePerenToken ClosePeren;

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
