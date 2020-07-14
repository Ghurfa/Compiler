using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class PrimitiveTypeExpression : PrimaryExpression
    {
        public readonly IToken PrimitiveType;
        public PrimitiveTypeExpression(TokenCollection tokens, IToken primitiveToken)
        {
            PrimitiveType = primitiveToken;
        }
        public override string ToString()
        {
            return PrimitiveType.ToString();
        }
    }
}
