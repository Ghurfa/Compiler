using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class ReturnStatement : Statement
    {
        public ReturnKeywordToken ReturnKeyword { get; private set; }
        public Expression Expression { get; private set; }
        public SemicolonToken? Semicolon { get; private set; }

        public ReturnStatement(TokenCollection tokens)
        {
            ReturnKeyword = tokens.PopToken<ReturnKeywordToken>();
            Expression = Expression.ReadExpression(tokens);
            Semicolon = tokens.EnsureValidStatementEnding();
        }

        public override string ToString()
        {
            string ret = "";
            ret += ReturnKeyword.ToString();
            ret += Expression.ToString();
            ret += Semicolon?.ToString();
            return ret;
        }
    }
}
