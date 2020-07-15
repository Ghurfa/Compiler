using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class UnaryPlusExpression : UnaryExpression
    {
        public readonly PlusToken UnaryPlus;
        public readonly UnaryExpression Expression;

        public override IToken LeftToken => UnaryPlus;
        public override IToken RightToken => Expression.RightToken;

        public UnaryPlusExpression(TokenCollection tokens, PlusToken? unaryPlus = null, UnaryExpression expression = null)
        {
            UnaryPlus = unaryPlus == null ? tokens.PopToken<PlusToken>() : (PlusToken)unaryPlus;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
