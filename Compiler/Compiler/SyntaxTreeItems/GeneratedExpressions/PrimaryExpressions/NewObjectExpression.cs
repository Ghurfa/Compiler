using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Compiler.SyntaxTreeItems.Statements;

namespace Compiler.SyntaxTreeItems
{
    public class NewObjectExpression : PrimaryExpression, ICompleteStatement
    {
        public readonly NewKeywordToken NewKeyword;
        public readonly Type Type;
        public readonly OpenPerenToken OpenPeren;
        public readonly ArgumentList Arguments;
        public readonly ClosePerenToken ClosePeren;

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
            ret += Type.ToString();
            ret += OpenPeren.ToString();
            ret += Arguments.ToString();
            ret += ClosePeren.ToString();
            return ret;
        }
    }
}
