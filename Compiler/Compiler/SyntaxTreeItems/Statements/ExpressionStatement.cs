using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class ExpressionStatement : Statement
    {
        public readonly Expression Expression;
        public ExpressionStatement(TokenCollection tokens)
        {
            var expression = SyntaxTreeItems.Expression.ReadExpression(tokens);
            if (expression is ICompleteStatement)
            {
                Expression = expression;
            }
            else throw new SyntaxTreeBuildingException(tokens.PeekToken());
        }
    }
}
