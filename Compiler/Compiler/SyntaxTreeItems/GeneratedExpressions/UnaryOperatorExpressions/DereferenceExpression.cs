using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class DereferenceExpression : UnaryExpression
    {
        public readonly AsteriskToken Dereference;
        public readonly UnaryExpression Expression;

        public DereferenceExpression(TokenCollection tokens, AsteriskToken? dereference = null, UnaryExpression expression = null)
        {
            Dereference = dereference == null ? tokens.PopToken<AsteriskToken>() : (AsteriskToken)dereference;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Dereference.ToString();
            ret += Expression.ToString();
            return ret;
        }
    }
}
