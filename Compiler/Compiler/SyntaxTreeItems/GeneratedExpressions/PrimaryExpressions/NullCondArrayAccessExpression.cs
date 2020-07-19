using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class NullCondArrayAccessExpression : PrimaryExpression, IAssignableExpression
    {
        public PrimaryExpression Array { get; private set; }
        public NullCondOpenBracketToken NullCondOpenBracket { get; private set; }
        public Expression Index { get; private set; }
        public CloseBracketToken CloseBracket { get; private set; }

        public NullCondArrayAccessExpression(TokenCollection tokens, PrimaryExpression array)
        {
            Array = array;
            NullCondOpenBracket = tokens.PopToken<NullCondOpenBracketToken>();
            Index = Expression.ReadExpression(tokens);
            CloseBracket = tokens.PopToken<CloseBracketToken>();
        }

        public override string ToString()
        {
            string ret = "";
            ret += Array.ToString();
            ret += NullCondOpenBracket.ToString();
            ret += Index.ToString();
            ret += CloseBracket.ToString();
            return ret;
        }
    }
}
