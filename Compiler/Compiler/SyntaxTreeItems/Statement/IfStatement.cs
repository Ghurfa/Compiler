using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class IfStatement : Statement
    {
        public readonly IfKeywordToken IfKeyword;
        public readonly Expression Condition;
        public readonly Statement IfTrue;
        public readonly ElseKeywordToken? ElseKeyword;
        public readonly Statement IfFalse;

        public override IToken LeftToken => IfKeyword;
        public override IToken RightToken => IfFalse.RightToken;

        public IfStatement(TokenCollection tokens, IfKeywordToken? ifKeyword = null, Expression condition = null, Statement ifTrue = null, ElseKeywordToken? elseKeyword = null)
        {
            IfKeyword = ifKeyword == null ? tokens.PopToken<IfKeywordToken>() : (IfKeywordToken)ifKeyword;
            Condition = condition == null ? Expression.ReadExpression(tokens) : condition;
            IfTrue = ifTrue == null ? Statement.ReadStatement(tokens) : ifTrue;
            if(elseKeyword == null) tokens.PopIfMatches(out ElseKeyword);
            else ElseKeyword = (ElseKeywordToken?)elseKeyword;
            if (ElseKeyword != null)
            {
                IfFalse = ifFalse == null ? Statement.ReadStatement(tokens) : ifFalse;
            }
            tokens.EnsureWhitespaceAfter(IfKeyword); 
            if (!(IfTrue is CodeBlock))
            {
                tokens.EnsureLineBreakAfter(Condition);
            }
            if (!(IfFalse is CodeBlock))
            {
                tokens.EnsureWhitespaceAfter(ElseKeyword); 
            }
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            
            
            
            
            
            return ret;
        }
    }
}
