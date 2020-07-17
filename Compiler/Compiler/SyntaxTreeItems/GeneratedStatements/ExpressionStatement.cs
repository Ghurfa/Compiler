using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; private set; }
        public SemicolonToken? Semicolon { get; private set; }

        public ExpressionStatement(TokenCollection tokens)
        {
            Expression = Expression.ReadExpression(tokens);
            Semicolon = tokens.EnsureValidStatementEnding();
        }

        public override string ToString()
        {
            string ret = "";
            ret += Expression.ToString();
            ret += Semicolon?.ToString();
            return ret;
        }
    }
}
