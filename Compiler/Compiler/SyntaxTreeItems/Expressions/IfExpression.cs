using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class IfExpression : Expression
    {
        public readonly Expression Condition;
        public readonly QuestionMarkToken QuestionMark;
        public readonly Expression IfTrue;
        public readonly BackslashToken Backslash;
        public readonly Expression IfFalse;

        public override IToken LeftToken => Condition.LeftToken;
        public override IToken RightToken => IfFalse.RightToken;

        public IfExpression(TokenCollection tokens, Expression condition = null, QuestionMarkToken? questionMark = null, Expression ifTrue = null, BackslashToken? backslash = null, Expression ifFalse = null)
        {
            Condition = condition == null ? Expression.ReadExpression(tokens) : condition;
            QuestionMark = questionMark == null ? tokens.PopToken<QuestionMarkToken>() : (QuestionMarkToken)questionMark;
            IfTrue = ifTrue == null ? Expression.ReadExpression(tokens) : ifTrue;
            Backslash = backslash == null ? tokens.PopToken<BackslashToken>() : (BackslashToken)backslash;
            IfFalse = ifFalse == null ? Expression.ReadExpression(tokens) : ifFalse;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            
            
            return ret;
        }
    }
}
