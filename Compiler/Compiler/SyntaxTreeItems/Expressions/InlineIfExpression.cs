using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class InlineIfExpression : Expression
    {
        public readonly Expression Condition;
        public readonly IToken QuestionMark;
        public readonly Expression IfTrue;
        public readonly IToken Backslash;
        public readonly Expression IfFalse;

        public InlineIfExpression(TokenCollection tokens, Expression condition)
        {
            Condition = condition;
            QuestionMark = tokens.PopToken(TokenType.QuestionMark);
            IfTrue = Expression.ReadExpression(tokens);
            Backslash = tokens.PopToken(TokenType.Backslash);
            IfFalse = Expression.ReadExpression(tokens);
        }
        public override string ToString()
        {
            return Condition.ToString() + " " + QuestionMark.Text + " " + IfTrue.ToString() + " " + Backslash.Text + " " + IfFalse.ToString();
        }
    }
}
