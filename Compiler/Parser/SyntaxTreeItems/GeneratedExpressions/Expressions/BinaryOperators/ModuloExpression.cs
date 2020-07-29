using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class ModuloExpression : Expression
    {
        public override int Precedence => 2;

        public Expression Left { get; private set; }
        public ModuloToken Modulo { get; private set; }
        public Expression Right { get; private set; }
        public override Expression LeftExpr { get => Left; set { Left = value; } }
        public override Expression RightExpr { get => Right; set { Right = value; } }

        public ModuloExpression(TokenCollection tokens, Expression left)
        {
            Left = left;
            Modulo = tokens.PopToken<ModuloToken>();
            Right = Expression.ReadExpression(tokens);
        }

        public override string ToString()
        {
            string ret = "";
            ret += Left.ToString();
            ret += Modulo.ToString();
            ret += Right.ToString();
            return ret;
        }
    }
}
