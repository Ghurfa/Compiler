using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseOrAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly BitwiseOrAssignToken BitwiseOrAssign;
        public readonly Expression From;

        public BitwiseOrAssignExpression(TokenCollection tokens, UnaryExpression to = null, BitwiseOrAssignToken? bitwiseOrAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            BitwiseOrAssign = bitwiseOrAssign == null ? tokens.PopToken<BitwiseOrAssignToken>() : (BitwiseOrAssignToken)bitwiseOrAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += BitwiseOrAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
