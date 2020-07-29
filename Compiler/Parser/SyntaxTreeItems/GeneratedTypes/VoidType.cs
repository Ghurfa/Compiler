using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class VoidType : Type
    {
        public VoidKeywordToken VoidKeyword { get; private set; }

        public VoidType(TokenCollection tokens)
        {
            VoidKeyword = tokens.PopToken<VoidKeywordToken>();
        }

        public override string ToString()
        {
            string ret = "";
            ret += VoidKeyword.ToString();
            return ret;
        }
    }
}
