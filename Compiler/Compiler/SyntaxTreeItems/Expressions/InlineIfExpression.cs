using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class InlineIfExpression : Expression
    {
        public readonly Expression Condition;
        public readonly Token QuestionMark;
        public readonly Expression IfTrue;
        public readonly Token Colon;
        public readonly Expression IfFalse;

        public InlineIfExpression(TokenCollection tokens, Expression condition, Token questionMark)
        {
            Condition = condition;
            QuestionMark = questionMark;
            IfTrue = Expression.ReadExpression(tokens);
            Colon = tokens.PopToken(TokenType.Colon);
            IfFalse = Expression.ReadExpression(tokens);
        }
    }
}
