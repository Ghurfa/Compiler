using Compiler.SyntaxTreeItems.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public class MethodBodyDeclaration
    {
        public readonly IToken OpenBrace;
        public readonly Statement[] Statements;
        public readonly IToken CloseBrace;
        public MethodBodyDeclaration(TokenCollection tokens)
        {
            OpenBrace = tokens.PopToken(TokenType.OpenCurly);

            var statements = new LinkedList<Statement>();
            while (!tokens.PopIfMatches(out CloseBrace, TokenType.CloseCurly))
            {
                statements.AddLast(Statement.ReadStatement(tokens));
            }
            Statements = statements.ToArray();
        }
    }
}
