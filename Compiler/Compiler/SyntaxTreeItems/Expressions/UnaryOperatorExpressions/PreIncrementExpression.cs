using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class PreIncrementExpression : UnaryExpression
    {
        public readonly IncrementToken PreIncrement;
        public readonly UnaryExpression Expression;

        public override IToken LeftToken => PreIncrement;
        public override IToken RightToken => Expression.RightToken;

        public PreIncrementExpression(TokenCollection tokens, IncrementToken? preIncrement = null, UnaryExpression expression = null)
        {
            PreIncrement = preIncrement == null ? tokens.PopToken<IncrementToken>() : (IncrementToken)preIncrement;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
