using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class ForBlock : Statement
    {
        public ForKeywordToken ForKeyword { get; private set; }
        public OpenPerenToken OpenPeren { get; private set; }
        public Statement StartStatement { get; private set; }
        public Expression ContinueExpr { get; private set; }
        public SemicolonToken? SecondSemicolon { get; private set; }
        public Statement IterateStatement { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }
        public Statement Body { get; private set; }

        public ForBlock(TokenCollection tokens)
        {
            ForKeyword = tokens.PopToken<ForKeywordToken>();
            OpenPeren = tokens.PopToken<OpenPerenToken>();
            StartStatement = Statement.ReadStatement(tokens);
            ContinueExpr = Expression.ReadExpression(tokens);
            SecondSemicolon = tokens.EnsureValidStatementEnding();
            IterateStatement = Statement.ReadStatement(tokens);
            ClosePeren = tokens.PopToken<ClosePerenToken>();
            Body = Statement.ReadStatement(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += ForKeyword.ToString();
            ret += OpenPeren.ToString();
            ret += StartStatement.ToString();
            ret += ContinueExpr.ToString();
            ret += SecondSemicolon?.ToString();
            ret += IterateStatement.ToString();
            ret += ClosePeren.ToString();
            ret += Body.ToString();
            return ret;
        }
    }
}
