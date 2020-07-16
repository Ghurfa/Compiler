using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class CastExpression : Expression
    {
        public override int Precedence => 1;

        public UnaryExpression Expression { get; private set; }
        public AsKeywordToken AsKeyword { get; private set; }
        public Type CastTo { get; private set; }
        public override Expression LeftExpr { get => Expression; set => throw new InvalidOperationException(); }
        public override Expression RightExpr { get => CastTo; set => throw new InvalidOperationException(); }

        public CastExpression(TokenCollection tokens, UnaryExpression expression = null, AsKeywordToken? asKeyword = null, Type castTo = null)
        {
            Expression = expression == null ? UnaryExpression.ReadUnaryExpression(tokens) : expression;
            AsKeyword = asKeyword == null ? tokens.PopToken<AsKeywordToken>() : (AsKeywordToken)asKeyword;
            CastTo = castTo == null ? Type.ReadType(tokens) : castTo;
        }

        public override string ToString()
        {
            string ret = "";
            ret += Expression.ToString();
            ret += " ";
            ret += AsKeyword.ToString();
            ret += " ";
            ret += CastTo.ToString();
            return ret;
        }
    }
}
