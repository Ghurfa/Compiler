using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class NullCondArrayAccessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression Array;
        public readonly NullCondOpenBracketToken NullCondOpenBracket;
        public readonly Expression Index;
        public readonly CloseBracketToken CloseBracket;

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
            ret += NullCondOpenBracket.ToString();
            ret += Index.ToString();
            ret += CloseBracket.ToString();
            return ret;
        }
    }
}
