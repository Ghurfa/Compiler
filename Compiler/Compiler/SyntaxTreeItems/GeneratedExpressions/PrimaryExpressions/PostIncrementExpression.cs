using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class PostIncrementExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly PrimaryExpression BaseExpression;
        public readonly IncrementToken Increment;

        public PostIncrementExpression(TokenCollection tokens, PrimaryExpression baseExpression = null, IncrementToken? increment = null)
        {
            BaseExpression = baseExpression == null ? PrimaryExpression.ReadPrimaryExpression(tokens) : baseExpression;
            Increment = increment == null ? tokens.PopToken<IncrementToken>() : (IncrementToken)increment;
        }

        public override string ToString()
        {
            string ret = "";
            ret += BaseExpression.ToString();
            ret += Increment.ToString();
            return ret;
        }
    }
}
