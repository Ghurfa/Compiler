using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public abstract class UnaryExpression : Expression
    {
        public static UnaryExpression ReadUnaryExpression(LinkedList<Token> tokens)
        {
            throw new NotImplementedException();
        }
    }
}
