using System;
using System.Collections.Generic;
using System.Linq;
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

        public IfExpression(TokenCollection tokens, Expression condition)
        {
            Condition = condition;
            QuestionMark = tokens.PopToken<QuestionMarkToken>();
            IfTrue = Expression.ReadExpression(tokens);
            Backslash = tokens.PopToken<BackslashToken>();
            IfFalse = Expression.ReadExpression(tokens);
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
