using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PostDecrementExpression : PrimaryExpression, ICompleteStatement
    {
        public PrimaryExpression BaseExpression { get; private set; }
        public DecrementToken Decrement { get; private set; }

        public PostDecrementExpression(TokenCollection tokens, PrimaryExpression baseExpression)
        {
            BaseExpression = baseExpression;
            Decrement = tokens.PopToken<DecrementToken>();;
        }

        public override string ToString()
        {
            string ret = "";
            ret += BaseExpression.ToString();
            ret += " ";
            ret += Decrement.ToString();
            return ret;
        }
    }
}
