using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class TupleItem
    {
        public readonly Expression Expression;
        public readonly CommaToken? Comma;

        public  IToken LeftToken => Expression.LeftToken;
        public  IToken RightToken => Comma.RightToken;

        public TupleItem(TokenCollection tokens, Expression expression = null, CommaToken? comma = null)
        {
            Expression = expression == null ? Expression.ReadExpression(tokens) : expression;
            if(comma == null) tokens.PopIfMatches(out Comma);
            else Comma = (CommaToken?)comma;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            return ret;
        }
    }
}
