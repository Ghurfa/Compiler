using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class IfExpression : Expression
    {
        public readonly Expression Condition;
        public readonly QuestionMarkToken QuestionMark;
        public readonly Expression IfTrue;
        public readonly BackslashToken Backslash;
        public readonly Expression IfFalse;

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
            ret += Condition.ToString();
            ret += QuestionMark.ToString();
            ret += IfTrue.ToString();
            ret += Backslash.ToString();
            ret += IfFalse.ToString();
            return ret;
        }
    }
}
