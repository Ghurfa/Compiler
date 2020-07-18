using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PostDecrementExpression : PrimaryExpression, ICompleteStatement
    {
        private PrimaryExpression baseExpr;
        public PrimaryExpression BaseExpression { get => baseExpr; set { if (value is IAssignableExpression) baseExpr = value; else throw new InvalidIncrDecrOperand(value); } }
        public DecrementToken Decrement { get; private set; }

        public PostDecrementExpression(TokenCollection tokens, PrimaryExpression baseExpression)
        {
            BaseExpression = baseExpression;
            Decrement = tokens.PopToken<DecrementToken>();
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
