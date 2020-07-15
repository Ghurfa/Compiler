using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ModuloAssignExpression : Expression, ICompleteStatement
    {
        public readonly UnaryExpression To;
        public readonly ModuloAssignToken ModuloAssign;
        public readonly Expression From;

        public override IToken LeftToken => To.LeftToken;
        public override IToken RightToken => From.RightToken;

        public ModuloAssignExpression(TokenCollection tokens, UnaryExpression to = null, ModuloAssignToken? moduloAssign = null, Expression from = null)
        {
            To = to == null ? UnaryExpression.ReadUnaryExpression(tokens) : to;
            ModuloAssign = moduloAssign == null ? tokens.PopToken<ModuloAssignToken>() : (ModuloAssignToken)moduloAssign;
            From = from == null ? Expression.ReadExpression(tokens) : from;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}
