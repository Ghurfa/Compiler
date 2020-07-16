using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class DereferenceExpression : UnaryExpression
    {
        public AsteriskToken Dereference { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public DereferenceExpression(TokenCollection tokens, AsteriskToken? dereference = null, UnaryExpression expression = null)
        {
            Dereference = dereference == null ? tokens.PopToken<AsteriskToken>() : (AsteriskToken)dereference;
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Dereference.ToString();
            ret += " ";
            ret += Expression.ToString();
            return ret;
        }
    }
}
