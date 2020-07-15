using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ExpressionStatement : Statement
    {
        public readonly Expression Expression;
        public readonly SemicolonToken? Semicolon;

        public override IToken LeftToken => Expression.LeftToken;
        public override IToken RightToken => Semicolon.RightToken;

        public ExpressionStatement(TokenCollection tokens, Expression expression = null, SemicolonToken? semicolon = null)
        {
            Expression = expression == null ? Expression.ReadExpression(tokens) : expression;
            throw new InvalidStatementException();
            Semicolon = semicolon == null ? tokens.EnsureValidStatementEnding() : semicolon;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
