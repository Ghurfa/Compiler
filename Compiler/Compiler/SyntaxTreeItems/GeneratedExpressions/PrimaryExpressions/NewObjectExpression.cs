using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class NewObjectExpression : PrimaryExpression, ICompleteStatement
    {
        public NewKeywordToken NewKeyword { get; private set; }
        public Type Type { get; private set; }
        public OpenPerenToken OpenPeren { get; private set; }
        public ArgumentList Arguments { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }

        public NewObjectExpression(TokenCollection tokens, NewKeywordToken? newKeyword = null, Type type = null, OpenPerenToken? openPeren = null, ArgumentList arguments = null, ClosePerenToken? closePeren = null)
        {
            NewKeyword = newKeyword == null ? tokens.PopToken<NewKeywordToken>() : (NewKeywordToken)newKeyword;
            Type = type == null ? Type.ReadType(tokens) : type;
            OpenPeren = openPeren == null ? tokens.PopToken<OpenPerenToken>() : (OpenPerenToken)openPeren;
            Arguments = arguments == null ? new ArgumentList(tokens) : arguments;
            ClosePeren = closePeren == null ? tokens.PopToken<ClosePerenToken>() : (ClosePerenToken)closePeren;
        }

        public override string ToString()
        {
            string ret = "";
            ret += NewKeyword.ToString();
            ret += " ";
            ret += Type.ToString();
            ret += " ";
            ret += OpenPeren.ToString();
            ret += Arguments.ToString();
            ret += ClosePeren.ToString();
            return ret;
        }
    }
}
