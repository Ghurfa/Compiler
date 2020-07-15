using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems.Statements
{
    public class CodeBlock : Statement
    {
        public readonly OpenCurlyToken OpenCurly;
        public readonly Statement[] Statements;
        public readonly CloseCurlyToken CloseCurly;

        public CodeBlock(TokenCollection tokens)
        : this(tokens, tokens.PopToken<OpenCurlyToken>()) { }
        public CodeBlock(TokenCollection tokens, OpenCurlyToken openCurly)
        {
            OpenCurly = openCurly;

            var statements = new LinkedList<Statement>();
            while (!tokens.PopIfMatches(out CloseCurly))
            {
                statements.AddLast(ReadStatement(tokens));
            }
            Statements = statements.ToArray();
        }
    }
}
