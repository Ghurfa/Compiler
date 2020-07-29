using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class PreIncrementExpression : UnaryExpression
    {
        public IncrementToken PreIncrement { get; private set; }
        private UnaryExpression baseExpr;
        public UnaryExpression BaseExpression { get => baseExpr; set { if (value is IAssignableExpression) baseExpr = value; else throw new InvalidIncrDecrOperand(value); } }

        public PreIncrementExpression(TokenCollection tokens)
        {
            PreIncrement = tokens.PopToken<IncrementToken>();
            BaseExpression = UnaryExpression.ReadUnaryExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += PreIncrement.ToString();
            ret += BaseExpression.ToString();
            return ret;
        }
    }
}
