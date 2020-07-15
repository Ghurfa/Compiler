using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ForeachStatement : Statement
    {
        public readonly ForeachKeywordToken ForeachKeyword;
        public readonly OpenPerenToken OpenPeren;
        public readonly Expression IteratorDecl;
        public readonly InKeywordToken InKeyword;
        public readonly Expression IterateOn;
        public readonly ClosePerenToken ClosePeren;
        public readonly Statement Body;

        public override IToken LeftToken => ForeachKeyword;
        public override IToken RightToken => Body.RightToken;

        public ForeachStatement(TokenCollection tokens, ForeachKeywordToken? foreachKeyword = null, OpenPerenToken? openPeren = null, Expression iteratorDecl = null, InKeywordToken? inKeyword = null, Expression iterateOn = null, ClosePerenToken? closePeren = null, Statement body = null)
        {
            ForeachKeyword = foreachKeyword == null ? tokens.PopToken<ForeachKeywordToken>() : (ForeachKeywordToken)foreachKeyword;
            OpenPeren = openPeren == null ? tokens.PopToken<OpenPerenToken>() : (OpenPerenToken)openPeren;
            IteratorDecl = iteratorDecl == null ? Expression.ReadExpression(tokens) : iteratorDecl;
            InKeyword = inKeyword == null ? tokens.PopToken<InKeywordToken>() : (InKeywordToken)inKeyword;
            IterateOn = iterateOn == null ? Expression.ReadExpression(tokens) : iterateOn;
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
