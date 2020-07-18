using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class PreDecrementExpression : UnaryExpression
    {
        public DecrementToken PreDecrement { get; private set; }
        private UnaryExpression baseExpr;
        public UnaryExpression BaseExpression { get => baseExpr; set { if (value is IAssignableExpression) baseExpr = value; else throw new InvalidIncrDecrOperand(value); } }

        public PreDecrementExpression(TokenCollection tokens)
        {
            PreDecrement = tokens.PopToken<DecrementToken>();
            BaseExpression = UnaryExpression.ReadUnaryExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += PreDecrement.ToString();
            ret += BaseExpression.ToString();
            return ret;
        }
    }
}
