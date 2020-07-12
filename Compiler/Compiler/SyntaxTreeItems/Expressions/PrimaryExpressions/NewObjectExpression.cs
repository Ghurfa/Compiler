using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    //Apparently this is a valid statement in c#
    public class NewObjectExpression : PrimaryExpression, ICompleteStatement
    {
        public NewObjectExpression(TokenCollection tokens, Token newKeywordToken)
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
