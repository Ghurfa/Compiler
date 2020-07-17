using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ReturnStatement : Statement
    {
        public ReturnKeywordToken ReturnKeyword { get; private set; }
        public OpenPerenToken OpenPeren { get; private set; }
        public Expression Expression { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }

        public ReturnStatement(TokenCollection tokens)
        {
            ReturnKeyword = tokens.PopToken<ReturnKeywordToken>();;
            OpenPeren = tokens.PopToken<OpenPerenToken>();;
            Expression = Expression.ReadExpression(tokens);
            ClosePeren = tokens.PopToken<ClosePerenToken>();;
        }

        public override string ToString()
        {
            string ret = "";
            ret += ReturnKeyword.ToString();
            ret += " ";
            ret += OpenPeren.ToString();
            ret += Expression.ToString();
            ret += ClosePeren.ToString();
            return ret;
        }
    }
}
