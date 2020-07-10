using Compiler.SyntaxTreeItems.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public abstract class Expression
    {
        public static Expression ReadExpression(LinkedList<Token> tokens)
        {
            throw new NotImplementedException();
        }
    }
}
