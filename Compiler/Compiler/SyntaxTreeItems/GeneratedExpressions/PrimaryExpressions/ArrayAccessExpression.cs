using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ArrayAccessExpression : PrimaryExpression, IAssignableExpression
    {
        public PrimaryExpression Array { get; private set; }
        public OpenBracketToken OpenBracket { get; private set; }
        public Expression Index { get; private set; }
        public CloseBracketToken CloseBracket { get; private set; }

        public ArrayAccessExpression(TokenCollection tokens, PrimaryExpression array)
        {
            Array = array;
            OpenBracket = tokens.PopToken<OpenBracketToken>();;
            Index = Expression.ReadExpression(tokens);
            CloseBracket = tokens.PopToken<CloseBracketToken>();;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Array.ToString();
            ret += " ";
            ret += OpenBracket.ToString();
            ret += Index.ToString();
            ret += CloseBracket.ToString();
            return ret;
        }
    }
}
