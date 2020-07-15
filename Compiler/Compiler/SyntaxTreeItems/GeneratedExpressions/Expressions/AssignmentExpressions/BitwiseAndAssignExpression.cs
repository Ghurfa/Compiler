using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseAndAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly BitwiseAndAssignToken BitwiseAndAssign;
        public readonly Expression From;

        public BitwiseAndAssignExpression(TokenCollection tokens, UnaryExpression to = null, BitwiseAndAssignToken? bitwiseAndAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            BitwiseAndAssign = bitwiseAndAssign == null ? tokens.PopToken<BitwiseAndAssignToken>() : (BitwiseAndAssignToken)bitwiseAndAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += BitwiseAndAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
