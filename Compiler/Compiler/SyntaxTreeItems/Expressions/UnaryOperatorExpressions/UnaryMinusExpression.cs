using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class UnaryMinusExpression : UnaryExpression
    {
        public readonly MinusToken UnaryMinus;
        public readonly UnaryExpression Expression;

        public override IToken LeftToken => UnaryMinus;
        public override IToken RightToken => Expression.RightToken;

        public UnaryMinusExpression(TokenCollection tokens, MinusToken? unaryMinus = null, UnaryExpression expression = null)
        {
            UnaryMinus = unaryMinus == null ? tokens.PopToken<MinusToken>() : (MinusToken)unaryMinus;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
