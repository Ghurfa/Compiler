using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class NewObjectExpression : PrimaryExpression, ICompleteStatement
    {
        public NewKeywordToken NewKeyword { get; private set; }
        public Type Type { get; private set; }
        public OpenPerenToken OpenPeren { get; private set; }
        public ArgumentList Arguments { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }

        public NewObjectExpression(TokenCollection tokens, NewKeywordToken newKeyword)
        {
            NewKeyword = newKeyword;
            Type = Type.ReadType(tokens);
            OpenPeren = tokens.PopToken<OpenPerenToken>();
            Arguments = new ArgumentList(tokens);
            ClosePeren = tokens.PopToken<ClosePerenToken>();
        }

        public override string ToString()
        {
            string ret = "";
            ret += NewKeyword.ToString();
            ret += Type.ToString();
            ret += OpenPeren.ToString();
            ret += Arguments.ToString();
            ret += ClosePeren.ToString();
            return ret;
        }
    }
}
