using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MethodBodyDeclaration
    {
        public readonly Token OpenBrace;
        public readonly Statement[] Statements;
        public readonly Token CloseBrace;
        public MethodBodyDeclaration(TokenCollection tokens)
        {
            OpenBrace = tokens.PopToken(TokenType.OpenCurly);

            LinkedList<Statement> statements = new LinkedList<Statement>();
            while (!tokens.PopIfMatches(out CloseBrace, TokenType.CloseCurly))
            {
                statements.AddLast(Statement.ReadStatement(tokens));
            }
            Statements = statements.ToArray();
        }
    }
}
