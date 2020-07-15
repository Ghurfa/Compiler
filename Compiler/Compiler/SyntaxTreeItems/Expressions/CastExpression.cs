using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class CastExpression : Expression
    {
        public readonly UnaryExpression Expression;
        public readonly AsKeywordToken AsKeyword;
        public readonly Type CastTo;

        public override IToken LeftToken => Expression.LeftToken;
        public override IToken RightToken => CastTo.RightToken;

        public CastExpression(TokenCollection tokens, UnaryExpression expression = null, AsKeywordToken? asKeyword = null, Type castTo = null)
        {
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
            AsKeyword = asKeyword == null ? tokens.PopToken<AsKeywordToken>() : (AsKeywordToken)asKeyword;
            CastTo = castTo == null ? Type.ReadType(tokens) : castTo;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
