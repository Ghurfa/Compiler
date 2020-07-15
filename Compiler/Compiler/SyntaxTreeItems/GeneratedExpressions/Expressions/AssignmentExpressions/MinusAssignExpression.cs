using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class MinusAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly MinusAssignToken MinusAssign;
        public readonly Expression From;

        public MinusAssignExpression(TokenCollection tokens, UnaryExpression to = null, MinusAssignToken? minusAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            MinusAssign = minusAssign == null ? tokens.PopToken<MinusAssignToken>() : (MinusAssignToken)minusAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += MinusAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
