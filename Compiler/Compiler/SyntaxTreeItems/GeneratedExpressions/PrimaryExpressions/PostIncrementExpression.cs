using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PostIncrementExpression : PrimaryExpression, ICompleteStatement
    {
        public PrimaryExpression BaseExpression { get; private set; }
        public IncrementToken Increment { get; private set; }

        public PostIncrementExpression(TokenCollection tokens, PrimaryExpression baseExpression)
        {
            BaseExpression = baseExpression;
            Increment = tokens.PopToken<IncrementToken>();
        }

        public override string ToString()
        {
            string ret = "";
            ret += BaseExpression.ToString();
            ret += " ";
            ret += Increment.ToString();
            return ret;
        }
    }
}
