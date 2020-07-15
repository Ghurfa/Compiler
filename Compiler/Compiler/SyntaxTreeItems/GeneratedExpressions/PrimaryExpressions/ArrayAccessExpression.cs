using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class ArrayAccessExpression : PrimaryExpression
    {
        public readonly PrimaryExpression Array;
        public readonly OpenBracketToken OpenBracket;
        public readonly Expression Index;
        public readonly CloseBracketToken CloseBracket;

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
            ret += OpenBracket.ToString();
            ret += Index.ToString();
            ret += CloseBracket.ToString();
            return ret;
        }
    }
}
