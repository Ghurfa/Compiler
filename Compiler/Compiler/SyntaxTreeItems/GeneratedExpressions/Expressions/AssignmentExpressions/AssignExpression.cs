using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class AssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly AssignToken Assign;
        public readonly Expression From;

        public AssignExpression(TokenCollection tokens, UnaryExpression to = null, AssignToken? assign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            Assign = assign == null ? tokens.PopToken<AssignToken>() : (AssignToken)assign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += Assign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
