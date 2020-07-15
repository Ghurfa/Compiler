using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class SwitchStatement : Statement
    {
        public readonly SwitchKeywordToken SwitchKeyword;
        public readonly Expression SwitchOn;
        public readonly OpenBracketToken OpenBracket;
        public readonly CaseStatement[] CaseStatement;
        public readonly CloseBracketToken CloseBracket;

        public override IToken LeftToken => SwitchKeyword;
        public override IToken RightToken => CloseBracket;

        public SwitchStatement(TokenCollection tokens, SwitchKeywordToken? switchKeyword = null, Expression switchOn = null, OpenBracketToken? openBracket = null, CaseStatement[] caseStatement = null)
        {
            SwitchKeyword = switchKeyword == null ? tokens.PopToken<SwitchKeywordToken>() : (SwitchKeywordToken)switchKeyword;
            SwitchOn = switchOn == null ? Expression.ReadExpression(tokens) : switchOn;
            OpenBracket = openBracket == null ? tokens.PopToken<OpenBracketToken>() : (OpenBracketToken)openBracket;
            var caseStatementList = new LinkedList<CaseStatement>();
            if (caseStatement != null)
            {
                foreach (var item in caseStatement)
                {
                    caseStatementList.AddLast(item);
                }
            }
            while(!tokens.PopIfMatches(out CloseBracket))
            {
                var newItem = new CaseStatement(tokens);
                caseStatementList.AddLast(newItem);
            }
            CaseStatement = caseStatementList.ToArray();
            tokens.EnsureWhitespaceAfter(SwitchKeyword); 
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            
            return ret;
        }
    }
}
