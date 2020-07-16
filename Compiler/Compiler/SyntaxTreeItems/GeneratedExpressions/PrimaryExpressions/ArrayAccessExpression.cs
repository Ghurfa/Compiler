using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ArrayAccessExpression : PrimaryExpression
    {
        public PrimaryExpression Array { get; private set; }
        public OpenBracketToken OpenBracket { get; private set; }
        public Expression Index { get; private set; }
        public CloseBracketToken CloseBracket { get; private set; }

        public ArrayAccessExpression(TokenCollection tokens, PrimaryExpression array = null, OpenBracketToken? openBracket = null, Expression index = null, CloseBracketToken? closeBracket = null)
        {
            Array = array == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : array;
            OpenBracket = openBracket == null ? tokens.PopToken<OpenBracketToken>() : (OpenBracketToken)openBracket;
            Index = index == null ? Expression.ReadExpression(tokens) : index;
            CloseBracket = closeBracket == null ? tokens.PopToken<CloseBracketToken>() : (CloseBracketToken)closeBracket;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Array.ToString();
            ret += " ";
            ret += OpenBracket.ToString();
            ret += " ";
            ret += Index.ToString();
            ret += " ";
            ret += CloseBracket.ToString();
            return ret;
        }
    }
}
