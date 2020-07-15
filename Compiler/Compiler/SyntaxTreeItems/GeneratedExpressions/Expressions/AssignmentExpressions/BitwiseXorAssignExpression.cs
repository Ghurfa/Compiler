using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class BitwiseXorAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly BitwiseXorAssignToken BitwiseXorAssign;
        public readonly Expression From;

        public BitwiseXorAssignExpression(TokenCollection tokens, UnaryExpression to = null, BitwiseXorAssignToken? bitwiseXorAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            BitwiseXorAssign = bitwiseXorAssign == null ? tokens.PopToken<BitwiseXorAssignToken>() : (BitwiseXorAssignToken)bitwiseXorAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += BitwiseXorAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
