using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class PrimitiveTypeExpression : PrimaryExpression
    {
        public readonly Token PrimitiveType;
        public PrimitiveTypeExpression(TokenCollection tokens, Token primitiveToken)
        {
            PrimitiveType = primitiveToken;
        }
    }
}
