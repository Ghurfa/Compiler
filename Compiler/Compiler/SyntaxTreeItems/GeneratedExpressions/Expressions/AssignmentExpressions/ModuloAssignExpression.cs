using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class ModuloAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly ModuloAssignToken ModuloAssign;
        public readonly Expression From;

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
