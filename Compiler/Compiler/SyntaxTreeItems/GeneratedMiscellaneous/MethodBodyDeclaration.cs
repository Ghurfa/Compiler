using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MethodBodyDeclaration
    {
        public OpenCurlyToken OpenCurly { get; private set; }
        public Statement[] Statements { get; private set; }
        private CloseCurlyToken closeCurly;
        public CloseCurlyToken CloseCurly { get => closeCurly; private set { closeCurly = value; } }

        public MethodBodyDeclaration(TokenCollection tokens)
        {
            OpenCurly = tokens.PopToken<OpenCurlyToken>();;
            LinkedList<Statement> statementsList = new LinkedList<Statement>();
            while (!tokens.PopIfMatches(out this.closeCurly))
            {
                statementsList.AddLast(Statement.ReadStatement(tokens));
            }
            Statements = statementsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            ret += OpenCurly.ToString();
            ret += " ";
            ret += Statements.ToString();
            ret += " ";
            ret += CloseCurly.ToString();
            return ret;
        }
    }
}
