using Compiler.SyntaxTreeItems.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class IntLiteral : PrimaryExpression
    {
        public readonly IToken Token;

        public IntLiteral(TokenCollection tokens, IToken token)
        {
            Token = token;
        }
        public override string ToString()
        {
            return Token.ToString();
        }
    }
}
