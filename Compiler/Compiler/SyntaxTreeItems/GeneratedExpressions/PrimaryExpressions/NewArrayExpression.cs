using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class NewArrayExpression : PrimaryExpression, ICompleteStatement
    {
        public NewKeywordToken NewKeyword { get; private set; }
        public Type Type { get; private set; }

        public NewArrayExpression(TokenCollection tokens, NewKeywordToken newKeyword, Type type)
        {
            NewKeyword = newKeyword;
            Type = type;
        }

        public override string ToString()
        {
            string ret = "";
            ret += NewKeyword.ToString();
            ret += Type.ToString();
            return ret;
        }
    }
}
