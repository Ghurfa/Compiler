using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ForStatement : Statement
    {
        public readonly ForKeywordToken ForKeyword;
        public readonly OpenPerenToken OpenPeren;
        public readonly Statement StartStatement;
        public readonly Expression ContinueExpr;
        public readonly SemicolonToken? SecondSemicolon;
        public readonly Statement RepeatStatement;
        public readonly ClosePerenToken ClosePeren;
        public readonly Statement Body;

        public override IToken LeftToken => ForKeyword;
        public override IToken RightToken => Body.RightToken;

        public ForStatement(TokenCollection tokens, ForKeywordToken? forKeyword = null, OpenPerenToken? openPeren = null, Statement startStatement = null, Expression continueExpr = null, SemicolonToken? secondSemicolon = null, Statement repeatStatement = null, ClosePerenToken? closePeren = null, Statement body = null)
        {
            ForKeyword = forKeyword == null ? tokens.PopToken<ForKeywordToken>() : (ForKeywordToken)forKeyword;
            OpenPeren = openPeren == null ? tokens.PopToken<OpenPerenToken>() : (OpenPerenToken)openPeren;
            StartStatement = startStatement == null ? Statement.ReadStatement(tokens) : startStatement;
            ContinueExpr = continueExpr == null ? Expression.ReadExpression(tokens) : continueExpr;
            SecondSemicolon = secondSemicolon == null ? tokens.EnsureValidStatementEnding() : secondSemicolon;
            RepeatStatement = repeatStatement == null ? Statement.ReadStatement(tokens) : repeatStatement;
            ClosePeren = closePeren == null ? tokens.PopToken<ClosePerenToken>() : (ClosePerenToken)closePeren;
            Body = body == null ? Statement.ReadStatement(tokens) : body;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            
            
            
            
            
            return ret;
        }
    }
}
