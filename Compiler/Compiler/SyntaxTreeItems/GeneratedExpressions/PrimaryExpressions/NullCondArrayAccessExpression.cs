using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class NullCondArrayAccessExpression : PrimaryExpression, IAssignableExpression
    {
        public PrimaryExpression Array { get; private set; }
        public NullCondOpenBracketToken NullCondOpenBracket { get; private set; }
        public Expression Index { get; private set; }
        public CloseBracketToken CloseBracket { get; private set; }

        public NullCondArrayAccessExpression(TokenCollection tokens, PrimaryExpression array = null, NullCondOpenBracketToken? nullCondOpenBracket = null, Expression index = null, CloseBracketToken? closeBracket = null)
        {
            Array = array == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : array;
            NullCondOpenBracket = nullCondOpenBracket == null ? tokens.PopToken<NullCondOpenBracketToken>() : (NullCondOpenBracketToken)nullCondOpenBracket;
            Index = index == null ? Expression.ReadExpression(tokens) : index;
            CloseBracket = closeBracket == null ? tokens.PopToken<CloseBracketToken>() : (CloseBracketToken)closeBracket;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Array.ToString();
            ret += " ";
            ret += NullCondOpenBracket.ToString();
            ret += " ";
            ret += Index.ToString();
            ret += CloseBracket.ToString();
            return ret;
        }
    }
}
