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
        public MethodBodyDeclaration(LinkedList<Token> tokens)
        {
            OpenBrace = tokens.GetToken(TokenType.SyntaxChar, "{");

            LinkedList<Statement> statements = new LinkedList<Statement>();
            while (!tokens.PopIfMatches(out CloseBrace, TokenType.SyntaxChar, "}"))
            {
                statements.AddLast(readStatement(tokens));
            }
            Statements = statements.ToArray();
        }
        private Statement readStatement(LinkedList<Token> tokens)
        {
            throw new NotImplementedException();
        }
    }
}
