using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class MultiplyAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly MultiplyAssignToken MultiplyAssign;
        public readonly Expression From;

        public MultiplyAssignExpression(TokenCollection tokens, UnaryExpression to = null, MultiplyAssignToken? multiplyAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            MultiplyAssign = multiplyAssign == null ? tokens.PopToken<MultiplyAssignToken>() : (MultiplyAssignToken)multiplyAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += MultiplyAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
