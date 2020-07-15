using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class ModuloExpression : Expression
    {
        public readonly Expression Left;
        public readonly ModuloToken Modulo;
        public readonly Expression Right;

        public override IToken LeftToken => Left.LeftToken;
        public override IToken RightToken => Right.RightToken;

        public ModuloExpression(TokenCollection tokens, Expression left = null, ModuloToken? modulo = null, Expression right = null)
        {
            Left = left == null ? Expression.ReadExpression(tokens) : left;
            Modulo = modulo == null ? tokens.PopToken<ModuloToken>() : (ModuloToken)modulo;
            Right = right == null ? Expression.ReadExpression(tokens) : right;
        }

        public override string ToString()
        {
            string ret = "";
            
            
            
            return ret;
        }
    }
}