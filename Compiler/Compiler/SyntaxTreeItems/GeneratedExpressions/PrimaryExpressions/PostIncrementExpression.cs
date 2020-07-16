using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class PostIncrementExpression : PrimaryExpression, ICompleteStatement
    {
        public PrimaryExpression BaseExpression { get; private set; }
        public IncrementToken Increment { get; private set; }

        public PostIncrementExpression(TokenCollection tokens, PrimaryExpression baseExpression = null, IncrementToken? increment = null)
        {
            BaseExpression = baseExpression == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : baseExpression;
            Increment = increment == null ? tokens.PopToken<IncrementToken>() : (IncrementToken)increment;
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
