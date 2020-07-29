using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class CastExpression : Expression
    {
        public override int Precedence => 1;

        public UnaryExpression Expression { get; private set; }
        public AsKeywordToken AsKeyword { get; private set; }
        public Type CastTo { get; private set; }
        public override Expression LeftExpr { get => Expression; set => throw new InvalidOperationException(); }
        public override Expression RightExpr { get => CastTo; set => throw new InvalidOperationException(); }

        public CastExpression(TokenCollection tokens, UnaryExpression expression)
        {
            Expression = expression;
            AsKeyword = tokens.PopToken<AsKeywordToken>();
            CastTo = Type.ReadType(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Expression.ToString();
            ret += AsKeyword.ToString();
            ret += CastTo.ToString();
            return ret;
        }
    }
}
