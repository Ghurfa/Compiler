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

        public CodeBlock(TokenCollection tokens)
        : this(tokens, tokens.PopToken(TokenType.SyntaxChar, "{")) { }
        public CodeBlock(TokenCollection tokens, Token openBrace)
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
