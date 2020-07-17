using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class DereferenceExpression : UnaryExpression, IAssignableExpression
    {
        public AsteriskToken Dereference { get; private set; }
        public UnaryExpression Expression { get; private set; }

        public DereferenceExpression(TokenCollection tokens)
        {
            Dereference = tokens.PopToken<AsteriskToken>();
            Expression = UnaryExpression.ReadUnaryExpression(tokens);
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
