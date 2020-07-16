using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public abstract class UnaryExpression : Expression
    {
        public override int Precedence => 0;
        public override Expression LeftExpr { get => null; set => throw new InvalidOperationException(); }
        public override Expression RightExpr { get => null; set => throw new InvalidOperationException(); }
        public static UnaryExpression ReadUnaryExpression(TokenCollection tokens)
        {
            switch(tokens.PeekToken())
            {
                case PlusToken _: return new UnaryPlusExpression(tokens);
                case MinusToken _: return new UnaryMinusExpression(tokens);
                case IncrementToken _: return new PreIncrementExpression(tokens);
                case DecrementToken _: return new PreDecrementExpression(tokens);
                case NotToken _: return new LogicalNotExpression(tokens);
                case BitwiseNotToken _: return new BitwiseNotExpression(tokens);
                case AsteriskToken _: return new DereferenceExpression(tokens);
                default: return PrimaryExpression.ReadPrimaryExpression(tokens);
            }
        }
    }
}
