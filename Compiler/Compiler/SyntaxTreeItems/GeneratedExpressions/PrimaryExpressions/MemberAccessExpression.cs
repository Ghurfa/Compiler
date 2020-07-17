using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MemberAccessExpression : PrimaryExpression, IAssignableExpression
    {
        public PrimaryExpression BaseExpression { get; private set; }
        public DotToken Dot { get; private set; }
        public IdentifierToken Item { get; private set; }

        public MemberAccessExpression(TokenCollection tokens, PrimaryExpression baseExpression)
        {
            BaseExpression = baseExpression;
            Dot = tokens.PopToken<DotToken>();
            Item = tokens.PopToken<IdentifierToken>();
        }

        public override string ToString()
        {
            string ret = "";
            ret += BaseExpression.ToString();
            ret += Dot.ToString();
            ret += Item.ToString();
            return ret;
        }
    }
}
