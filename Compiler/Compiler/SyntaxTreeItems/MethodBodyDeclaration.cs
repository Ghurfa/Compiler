using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class MethodBodyDeclaration
    {
        public readonly OpenCurlyToken OpenCurly;
        public readonly Statement[] Statements;
        public readonly CloseCurlyToken CloseCurly;

        public  IToken LeftToken => OpenCurly;
        public  IToken RightToken => CloseCurly;

        public MethodBodyDeclaration(TokenCollection tokens, OpenCurlyToken? openCurly = null, Statement[] statements = null)
        {
            OpenCurly = openCurly == null ? tokens.PopToken<OpenCurlyToken>() : (OpenCurlyToken)openCurly;
            var statementsList = new LinkedList<Statement>();
            if (statements != null)
            {
                foreach (var item in statements)
                {
                    statementsList.AddLast(item);
                }
            }
            while(!tokens.PopIfMatches(out CloseCurly))
            {
                var newItem = Statement.ReadStatement(tokens);
                statementsList.AddLast(newItem);
            }
            Statements = statementsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            
            return ret;
        }
    }
}
