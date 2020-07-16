using System;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class ModuloAssignExpression : Expression, ICompleteStatement
    {
        public UnaryExpression To { get; private set; }
        public ModuloAssignToken ModuloAssign { get; private set; }
        public Expression From { get; private set; }

        public override int Precedence => 14;

        public override Expression LeftExpr { get => To; set => throw new InvalidOperationException() }
        public override Expression RightExpr { get => To; set => throw new InvalidOperationException() }

        public ModuloAssignExpression(TokenCollection tokens, UnaryExpression to = null, ModuloAssignToken? moduloAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            ModuloAssign = moduloAssign == null ? tokens.PopToken<ModuloAssignToken>() : (ModuloAssignToken)moduloAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            ret += To.ToString();
            ret += ModuloAssign.ToString();
            ret += From.ToString();
            return ret;
        }
    }
}
