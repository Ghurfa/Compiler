using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PostIncrementExpression : PrimaryExpression, ICompleteStatement
    {
        private PrimaryExpression baseExpr;
        public PrimaryExpression BaseExpression { get => baseExpr; set { if (value is IAssignableExpression) baseExpr = value; else throw new InvalidIncrDecrOperand(value); } }
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
