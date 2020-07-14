using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class CodeBlock : Statement
    {
        public readonly IToken OpenBrace;
        public readonly Statement[] Statements;
        public readonly IToken CloseBrace;

        public CodeBlock(TokenCollection tokens)
        : this(tokens, tokens.PopToken(TokenType.OpenCurly)) { }
        public CodeBlock(TokenCollection tokens, IToken openBrace)
        {
            OpenBrace = openBrace;

            var statements = new LinkedList<Statement>();
            while (!tokens.PopIfMatches(out CloseBrace, TokenType.CloseCurly))
            {
                statements.AddLast(ReadStatement(tokens));
            }
            Statements = statements.ToArray();
        }
    }
}
