using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class CodeBlock : Statement
    {
        public readonly Token OpenBrace;
        public readonly Statement[] Statements;
        public readonly Token CloseBrace;

        public CodeBlock(LinkedList<Token> tokens)
        : this(tokens, tokens.GetToken(TokenType.SyntaxChar, "{")) { }
        public CodeBlock(LinkedList<Token> tokens, Token openBrace)
        {
            OpenBrace = openBrace;

            LinkedList<Statement> statements = new LinkedList<Statement>();
            while (!tokens.PopIfMatches(out CloseBrace, TokenType.SyntaxChar, "}"))
            {
                statements.AddLast(ReadStatement(tokens));
            }
            Statements = statements.ToArray();
        }
    }
}
