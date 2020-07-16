using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class IfExpression : Expression
    {
        public override int Precedence => 13;

        public Expression Condition { get; private set; }
        public QuestionMarkToken QuestionMark { get; private set; }
        public Expression IfTrue { get; private set; }
        public BackslashToken Backslash { get; private set; }
        public Expression IfFalse { get; private set; }
        public override Expression LeftExpr { get => Condition; set { Condition = value; } }
        public override Expression RightExpr { get => IfFalse; set { IfFalse = value; } }

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
            ret += " ";
            ret += QuestionMark.ToString();
            ret += " ";
            ret += IfTrue.ToString();
            ret += " ";
            ret += Backslash.ToString();
            ret += " ";
            ret += IfFalse.ToString();
            return ret;
        }
    }
}
