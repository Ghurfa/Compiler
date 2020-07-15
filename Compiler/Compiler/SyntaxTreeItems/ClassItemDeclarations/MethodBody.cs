using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MethodBodyDeclaration
    {
        public readonly OpenCurlyToken OpenCurly;
        public readonly Statement[] Statements;
        public readonly CloseCurlyToken CloseCurly;
        public MethodBodyDeclaration(TokenCollection tokens)
        {
            OpenCurly = tokens.PopToken<OpenCurlyToken>();

            var statements = new LinkedList<Statement>();
            while (!tokens.PopIfMatches(out CloseCurly))
            {
                statements.AddLast(Statement.ReadStatement(tokens));
            }
            Statements = statements.ToArray();
        }
    }
}
