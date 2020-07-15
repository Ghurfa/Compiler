using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class CastExpression : Expression
    {
        public readonly UnaryExpression Expression;
        public readonly AsKeywordToken AsKeyword;
        public readonly Type CastTo;

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
            ret += AsKeyword.ToString();
            ret += CastTo.ToString();
            return ret;
        }
    }
}
