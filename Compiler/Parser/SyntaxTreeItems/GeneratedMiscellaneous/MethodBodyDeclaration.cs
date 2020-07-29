using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public class MethodBodyDeclaration
    {
        public OpenCurlyToken OpenCurly { get; private set; }
        public Statement[] Statements { get; private set; }
        private CloseCurlyToken closeCurly;
        public CloseCurlyToken CloseCurly { get => closeCurly; private set { closeCurly = value; } }

        public MethodBodyDeclaration(TokenCollection tokens)
        {
            OpenCurly = tokens.PopToken<OpenCurlyToken>();
            LinkedList<Statement> statementsList = new LinkedList<Statement>();
            while (!tokens.PopIfMatches(out this.closeCurly))
            {
                var add = Statement.ReadStatement(tokens);
                statementsList.AddLast(add);
            }
            Statements = statementsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            ret += OpenCurly.ToString();
            foreach (var item in Statements)
            {
                ret += item.ToString();
            }
            ret += CloseCurly.ToString();
            return ret;
        }
    }
}
