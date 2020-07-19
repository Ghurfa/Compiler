using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class NullCondMemberAccessExpression : PrimaryExpression, IAssignableExpression
    {
        public PrimaryExpression BaseExpression { get; private set; }
        public NullCondDotToken NullCondDot { get; private set; }
        public IdentifierToken Item { get; private set; }

        public NullCondMemberAccessExpression(TokenCollection tokens, PrimaryExpression baseExpression)
        {
            BaseExpression = baseExpression;
            NullCondDot = tokens.PopToken<NullCondDotToken>();
            Item = tokens.PopToken<IdentifierToken>();
        }

        public override string ToString()
        {
            string ret = "";
            ret += BaseExpression.ToString();
            ret += NullCondDot.ToString();
            ret += Item.ToString();
            return ret;
        }
    }
}
