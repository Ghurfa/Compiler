using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class DereferenceExpression : UnaryExpression, IAssignableExpression
    {
        public AsteriskToken Dereference { get; private set; }
        public UnaryExpression BaseExpression { get; private set; }

        public DereferenceExpression(TokenCollection tokens)
        {
            Dereference = tokens.PopToken<AsteriskToken>();
            BaseExpression = UnaryExpression.ReadUnaryExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Dereference.ToString();
            ret += BaseExpression.ToString();
            return ret;
        }
    }
}
